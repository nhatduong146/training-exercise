using ConcurrencyLab.Domain.Entities;
using ConcurrencyLab.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyLab.Services.ProductService
{
    public class ProductService(AppDbContext appDbContext, 
        DbContextOptions<AppDbContext> appDbContextOptions) : IProductService
    {
        public List<Product> GetProducts()
        {
            return appDbContext.Products.ToList();
        }

        public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken)
        {
            return await appDbContext.Products.ToListAsync(cancellationToken);
        }

        public async Task<List<Product>> GetProductsInChunksAsync(CancellationToken cancellationToken)
        {
            var totalCount = await appDbContext.Products.CountAsync(cancellationToken);
            var pageSize = 100000; // Process 1000 records per task
            var tasks = new List<Task<List<Product>>>();

            for (int i = 0; i < totalCount; i += pageSize)
            {
                tasks.Add(FetchProductsChunk(i, pageSize, cancellationToken));
            }

            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x).ToList();
        }

        private async Task<List<Product>> FetchProductsChunk(int skip, int take, CancellationToken cancellationToken)
        {
            using var dbContext = new AppDbContext(appDbContextOptions);
            return await dbContext.Products
                .OrderBy(o => o.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }
    }
}
