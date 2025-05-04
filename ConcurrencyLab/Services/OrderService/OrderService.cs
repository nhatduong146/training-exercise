using ConcurrencyLab.Domain.Entities;
using ConcurrencyLab.Infrastructure.DbContexts;
using ConcurrencyLab.Infrastructure.Services.OrderQueue;
using ConcurrencyLab.ViewModels.Request;
using ConcurrencyLab.ViewModels.Response;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConcurrencyLab.Services.OrderService
{
    public class OrderService(OrderQueue orderQueue, 
        AppDbContext appDbContext) : IOrderService
    {
        public string CreateOrder(CreateOrderRequest request)
        {
            orderQueue.Enqueue(request);

            return "Order is processing...";
        }

        public async Task ProcessOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(_ => _.Id == request.ProductId, cancellationToken);

            if (product == null)
                return;

            var newOrder = new Order
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = product.Price * request.Quantity,
            };

            if (product.Quantity < request.Quantity)
            {
                newOrder.Status = "Failed";
                newOrder.FailureReason = "Product is out of stock";

                Console.WriteLine("Product is out of stock");
            }
            else
            {
                newOrder.Status = "Success";
                product.Quantity = product.Quantity - request.Quantity;

                Console.WriteLine("Order is processed successfully");
            }

            appDbContext.Orders.Add(newOrder);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task ProcessOrderPessmisticConcurenncyAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var strategy = appDbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await appDbContext.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var product = await appDbContext.Products.FromSqlRaw(
                        "SELECT * FROM Product WITH (UPDLOCK, ROWLOCK) WHERE Id = {0}", request.ProductId).FirstOrDefaultAsync(cancellationToken);

                    if (product == null)
                        return;

                    var newOrder = new Order
                    {
                        ProductId = request.ProductId,
                        Quantity = request.Quantity,
                        TotalPrice = product.Price * request.Quantity,
                    };

                    if (product.Quantity < request.Quantity)
                    {
                        newOrder.Status = "Failed";
                        newOrder.FailureReason = "Product is out of stock";

                        Console.WriteLine("Product is out of stock");
                    }
                    else
                    {
                        newOrder.Status = "Success";
                        product.Quantity = product.Quantity - request.Quantity;

                        Console.WriteLine("Order is processed successfully");
                    }

                    appDbContext.Orders.Add(newOrder);
                    await appDbContext.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            });
        }

        public async Task ProcessOrderOptimisticConcurenncyAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(_ => _.Id == request.ProductId, cancellationToken);

            if (product == null)
                return;

            var newOrder = new Order
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                TotalPrice = product.Price * request.Quantity,
            };

            if (product.Quantity < request.Quantity)
            {
                newOrder.Status = "Failed";
                newOrder.FailureReason = "Product is out of stock";
            }
            else
            {
                newOrder.Status = "Success";
                product.Quantity = product.Quantity - request.Quantity;
            }

            appDbContext.Orders.Add(newOrder);

            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    await appDbContext.SaveChangesAsync(cancellationToken);
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.FirstOrDefault(x => x.Entity is Product)
                        ?? throw ex;

                    var dbValue = await entry.GetDatabaseValuesAsync(cancellationToken)
                        ?? throw new Exception("The entity is deleted from the database in the meanwhile.");

                    // Get the updated quantity from the database
                    var databaseProduct = dbValue.ToObject() as Product
                        ?? throw new Exception("Error converting dbValue to Product");

                    var databaseQuantity = databaseProduct.Quantity;

                    // Check stock again based on the database value
                    if (databaseQuantity < request.Quantity)
                    {
                        newOrder.Status = "RetryFaield";
                        newOrder.FailureReason = "RetryFaield - Product is out of stock";
                        entry.State = EntityState.Unchanged;
                    }
                    else
                    {
                        // Update the product's quantity based on the database value
                        product.Quantity = databaseQuantity - request.Quantity;
                        entry.OriginalValues.SetValues(dbValue); //Update original values.
                    }
                }
            }
        }

        public async Task<List<OrderResponse>> GetOrdersSequentiallyAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            var orders = await appDbContext.Orders.AsNoTracking()
               .Select(_ => new OrderResponse
               {
                   Id = _.Id,
                   Quantity = _.Quantity,
                   TotalPrice = _.TotalPrice,
                   Status = _.Status,
                   FailureReason = _.FailureReason,
               }).ToListAsync(cancellationToken);


            orders.ForEach(async (order) =>
            {
                try
                {
                    var response = await httpClient.GetAsync("https://localhost:7142/products");
                    string json = await response.Content.ReadAsStringAsync();

                    var products = JsonConvert.DeserializeObject<List<Product>>(json);
                    if (products != null && products.Count != 0)
                    {
                        order.ProductName = products[0].Name;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch data");
                }
            });

            return orders;
        }

        public async Task<List<OrderResponse>> GetOrdersParallelAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();

            var orders = await appDbContext.Orders.AsNoTracking()
               .Include(_ => _.Product)
               .Select(_ => new OrderResponse
               {
                   Id = _.Id,
                   Quantity = _.Quantity,
                   TotalPrice = _.TotalPrice,
                   Status = _.Status,
                   FailureReason = _.FailureReason,
                   ProductName = _.Product.Name,
               }).ToListAsync(cancellationToken);


            await Parallel.ForEachAsync(orders, async (order, CancellationToken) =>
            {
                try
                {
                    var response = await httpClient.GetAsync("https://67d581e6d2c7857431f09b90.mockapi.io/posts/comments");
                    string json = await response.Content.ReadAsStringAsync();

                    var posts = JsonConvert.DeserializeObject<List<Product>>(json);
                    if (posts != null && posts.Count != 0)
                    {
                        order.ProductName = posts[0].Name;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to fetch data");
                }
            });

            return orders;
        }

        public IReadOnlyCollection<Order> GetUnpaidOrders(CancellationToken cancellationToken)
        {
            return
            [    
                new() { Id = new Guid(), TotalPrice = 10000000 },
                new() { Id = new Guid(), TotalPrice = 20000000 },
                new() { Id = new Guid(), TotalPrice = 30000000 },
                new() { Id = new Guid(), TotalPrice = 40000000 },
                new() { Id = new Guid(), TotalPrice = 50000000 },
                new() { Id = new Guid(), TotalPrice = 60000000 },
                new() { Id = new Guid(), TotalPrice = 70000000 },
                new() { Id = new Guid(), TotalPrice = 80000000 },
                new() { Id = new Guid(), TotalPrice = 90000000 },
                new() { Id = new Guid(), TotalPrice = 100000000 }
            ];
        }
    }
}
