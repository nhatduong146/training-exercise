using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RESTfullAPI.Domain.Entities;
using RESTfullAPI.Exceptions;
using RESTfullAPI.Extensions;
using RESTfullAPI.Infrastructure.DbContexts;
using RESTfullAPI.ViewModels.Pagination.Requests;
using RESTfullAPI.ViewModels.Pagination.Responses;
using RESTfullAPI.ViewModels.Product.Requests;
using RESTfullAPI.ViewModels.Product.Responses;

namespace RESTfullAPI.Services.ProductService;

public class ProductService(AppDbContext appDbContext) : IProductService
{
    public async Task<PaginatedResponse<ProductResponse>> GetAllAsync(PaginationFilterRequest request, CancellationToken cancellationToken)
    {
        return await appDbContext.Products
            .AsNoTracking()
            .Where(_ => request.SearchKeyword.IsNullOrEmpty() || _.Name.Contains(request.SearchKeyword))
            .ToPaginatedResponseAsync<Product, ProductResponse>(request, cancellationToken);
    }

    public async Task<CursorPaginatedResponse<ProductResponse>> GetAllAsync(CursorPaginationFilterRequest request, CancellationToken cancellationToken)
    {
        return await appDbContext.Products
           .AsNoTracking()
           .Where(_ => request.SearchKeyword.IsNullOrEmpty() || _.Name.Contains(request.SearchKeyword))
           .ToCursorPaginatedResponseAsync<Product, ProductResponse>(request, cancellationToken);
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products
           .AsNoTracking()
           .FirstOrDefaultAsync(p => p.Id == id, cancellationToken) 
           ?? throw new BadRequestException("Product not found");

        return product.Adapt<ProductResponse>();
    }

    public async Task CreateAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = new Product();
        request.Adapt(product);

        appDbContext.Products.Add(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products
           .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
           ?? throw new BadRequestException("Product not found");

        request.Adapt(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task PatchAsync(Guid id, PatchProductRequest request, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products
           .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
           ?? throw new BadRequestException("Product not found");

        request.Adapt(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products
           .FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
           ?? throw new BadRequestException("Product not found");

        appDbContext.Products.Remove(product);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
