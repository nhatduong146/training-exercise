
using ConcurrencyLab.Infrastructure.Services.PaymentGateway;
using ConcurrencyLab.Services.OrderService;

namespace ConcurrencyLab.Services.PaymentService
{
    public class PaymentServiceV3 : IPaymentService
    {
        private readonly IPaymentGatewayClient _paymentGatewayClient;
        private readonly IOrderService _orderService;

        public PaymentServiceV3(IPaymentGatewayClient paymentGatewayClient, IOrderService orderService)
        {
            _paymentGatewayClient = paymentGatewayClient;
            _orderService = orderService;
        }

        public async Task ProcessOrderPaymentAsync(CancellationToken cancellationToken)
        {
            var unpaidOrders = _orderService.GetUnpaidOrders(cancellationToken);
            await _paymentGatewayClient.ExecuteOrderPaymentAsync(unpaidOrders, cancellationToken);
        }
    }
}
