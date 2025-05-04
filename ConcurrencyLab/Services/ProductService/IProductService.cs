using ConcurrencyLab.Domain.Entities;

namespace ConcurrencyLab.Services.ProductService
{
    public interface IProductService
    {
        public List<Product> GetProducts();

        public Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken);

        public Task<List<Product>> GetProductsInChunksAsync(CancellationToken cancellationToken);
    }
}
