using ConcurrencyLab.Services.OrderService;
using ConcurrencyLab.Services.PaymentService;
using ConcurrencyLab.ViewModels.Request;
using Microsoft.AspNetCore.Mvc;

namespace ConcurrencyLab.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController(IOrderService orderService, IPaymentService paymentService) : ControllerBase
    {
        public static int Count;

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            Interlocked.Increment(ref Count);
            Console.WriteLine(Count);

            var result = orderService.CreateOrder(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();

            var orders = await orderService.GetOrdersParallelAsync(cancellationToken);

            stopWatch.Stop();
            Console.WriteLine("API TIME: ", stopWatch.ElapsedMilliseconds);
            return Ok(orders);
        }

        [HttpPost("payments")]
        public async Task ProcessPayment(CancellationToken cancellationToken)
        {
            await paymentService.ProcessOrderPaymentAsync(cancellationToken);
        }
    }
}
