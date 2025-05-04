using ConcurrencyLab.Domain.Entities;
using ConcurrencyLab.ViewModels.Request;
using ConcurrencyLab.ViewModels.Response;

namespace ConcurrencyLab.Services.OrderService
{
    public interface IOrderService
    {
        string CreateOrder(CreateOrderRequest request);

        Task ProcessOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken);
        
        Task ProcessOrderPessmisticConcurenncyAsync(CreateOrderRequest request, CancellationToken cancellationToken);
        
        Task ProcessOrderOptimisticConcurenncyAsync(CreateOrderRequest request, CancellationToken cancellationToken);

        Task<List<OrderResponse>> GetOrdersSequentiallyAsync(CancellationToken cancellationToken);

        Task<List<OrderResponse>> GetOrdersParallelAsync(CancellationToken cancellationToken);

        IReadOnlyCollection<Order> GetUnpaidOrders(CancellationToken cancellationToken);
    }
}
