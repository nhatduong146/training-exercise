using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTfullAPI.Domain.Entities;
using RESTfullAPI.Infrastructure.DbContexts;

namespace RESTfullAPI.Controllers;

[ApiController]
[Route("api/tests")]
public class ProductControllerTest(AppDbContext appDbContext)
{
    [HttpGet("queryable-products")]
    public async Task<IEnumerable<Product>> GetQueyableProducts(CancellationToken cancellationToken)
    {
        var products = await appDbContext.Products.AsNoTracking()
            .AsQueryable()
            .Where(_ => _.Quantity > 0 && _.Price > 0)
            .Select(_ => new Product
            {
                Id = _.Id,
                Name = _.Name,
                Price = _.Price,
                Quantity = _.Quantity,
            })
            .ToListAsync(cancellationToken);

        return products;
    }

    [HttpGet("enumerable-products")]
    public IEnumerable<Product> GetEnumerableProducts(CancellationToken cancellationToken)
    {
        var products = appDbContext.Products.AsNoTracking()
            .AsEnumerable()
            .Where(_ => _.Quantity > 0 && _.Price > 0)
            .Select(_ => new Product
            {
                Id = _.Id,
                Name = _.Name,
                Price = _.Price,
                Quantity = _.Quantity,
            });

        return products;
    }

    [HttpGet("products")]
    public IEnumerable<Product> GetEnumerableProductsV2(CancellationToken cancellationToken)
    {
        var illegalNames = new[] { "Fake", "Illegal" };

        var products = appDbContext.Products.AsNoTracking()
            .Where(_ => _.Quantity > 0 && _.Price > 0)
            .Select(_ => new Product
            {
                Id = _.Id,
                Name = _.Name,
                Price = _.Price,
                Quantity = _.Quantity,
            })
            .AsEnumerable()
            .Where(_ => !illegalNames.Any(illegelName => _.Name.Contains(illegelName, StringComparison.OrdinalIgnoreCase)));

        foreach (var product in products)
            product.Price = product.Price * 0.1m;

        return products;
    }
}
