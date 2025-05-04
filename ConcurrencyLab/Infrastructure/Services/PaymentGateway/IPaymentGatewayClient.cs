using ConcurrencyLab.Domain.Entities;

namespace ConcurrencyLab.Infrastructure.Services.PaymentGateway
{
    public interface IPaymentGatewayClient
    {
        Task ExecuteOrderPaymentAsync(IReadOnlyCollection<Order> orders, CancellationToken cancellationToken);
    }
}
