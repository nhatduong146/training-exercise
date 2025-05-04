
using ConcurrencyLab.Services.OrderService;

namespace ConcurrencyLab.Services.PaymentService
{
    public class PaymentServiceV1(IOrderService orderService) : IPaymentService
    {
        public async Task ProcessOrderPaymentAsync(CancellationToken cancellationToken)
        {
            var unpaidOrders = orderService.GetUnpaidOrders(cancellationToken);

            var tasks = unpaidOrders.Select(async order =>
            {
                try
                {
                    using var httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri("https://67d581e6d2c7857431f09b90.mockapi.io/");

                    var response = await httpClient.PostAsJsonAsync($"/orders/payments", order);
                    response.EnsureSuccessStatusCode();

                    Console.WriteLine($"{await response.Content.ReadAsStringAsync()}");
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}
