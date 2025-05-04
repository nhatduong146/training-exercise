using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RESTfullAPI.Services.ProductService;
using RESTfullAPI.ViewModels.Pagination.Requests;
using RESTfullAPI.ViewModels.Pagination.Responses;
using RESTfullAPI.ViewModels.Product.Requests;
using RESTfullAPI.ViewModels.Product.Responses;

namespace RESTfullAPI.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<PaginatedResponse<ProductResponse>> GetProducts([FromQuery] PaginationFilterRequest request, CancellationToken cancellationToken)
    {
        return await productService.GetAllAsync(request, cancellationToken);
    }

    [HttpGet("cursors")]
    [EnableRateLimiting("UnauthenticatedApi")]
    public async Task<CursorPaginatedResponse<ProductResponse>> GetProducts([FromQuery] CursorPaginationFilterRequest request, CancellationToken cancellationToken)
    {
        return await productService.GetAllAsync(request, cancellationToken);
    }

    [HttpGet("{id}")]
    [DisableRateLimiting]
    public async Task<ProductResponse> GetProductById(Guid id, CancellationToken cancellationToken)
    {
        return await productService.GetByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public async Task CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        await productService.CreateAsync(request, cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        await productService.UpdateAsync(id, request, cancellationToken);
    }

    [HttpPatch("{id}")]
    public async Task PatchProduct(Guid id, [FromBody] PatchProductRequest request, CancellationToken cancellationToken)
    {
        await productService.PatchAsync(id, request, cancellationToken);

    }

    [HttpDelete("{id}")]
    public async Task DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        await productService.DeleteAsync(id, cancellationToken);
    }
}
