using ConcurrencyLab.Services.OrderService;

namespace ConcurrencyLab.Services.PaymentService
{
    public class PaymentServiceV2 : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;

        public PaymentServiceV2(IOrderService orderService)
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(4), // Refresh DNS // Default infinite
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2), // Cleanup unused
                MaxConnectionsPerServer = 5 // Concurrent TCP connections
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri("https://67d581e6d2c7857431f09b90.mockapi.io/");
            _orderService = orderService;
        }

        public async Task ProcessOrderPaymentAsync(CancellationToken cancellationToken)
        {
            var unpaidOrders = _orderService.GetUnpaidOrders(cancellationToken);
            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount - 1,
                CancellationToken = cancellationToken
            };

            await Parallel.ForEachAsync(unpaidOrders, options, async (order, ct) =>
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync($"/orders/payments", order, ct);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"{await response.Content.ReadAsStringAsync(ct)}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });
        }
    }
}
