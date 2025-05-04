using RESTfullAPI.ViewModels.Pagination.Requests;
using RESTfullAPI.ViewModels.Pagination.Responses;
using RESTfullAPI.ViewModels.Product.Requests;
using RESTfullAPI.ViewModels.Product.Responses;

namespace RESTfullAPI.Services.ProductService;

public interface IProductService
{
    Task<PaginatedResponse<ProductResponse>> GetAllAsync(PaginationFilterRequest request, CancellationToken cancellationToken);
    Task<CursorPaginatedResponse<ProductResponse>> GetAllAsync(CursorPaginationFilterRequest request, CancellationToken cancellationToken);
    Task<ProductResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken);
    Task PatchAsync(Guid id, PatchProductRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
