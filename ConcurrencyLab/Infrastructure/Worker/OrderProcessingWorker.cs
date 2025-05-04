using ConcurrencyLab.Infrastructure.Services.OrderQueue;
using ConcurrencyLab.Services.OrderService;

namespace ConcurrencyLab.Infrastructure.Worker
{
    public class OrderProcessingWorker(IServiceScopeFactory scopeFactory, OrderQueue orderQueue) : BackgroundService
    {
        private const int ORDER_BATCH_SIZE = 5;

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var orderRequests = orderQueue.DequeueBatch(ORDER_BATCH_SIZE);

                if (orderRequests.Count != 0)
                {
                    var tasks = orderRequests.Select(request =>
                    {
                        var scope = scopeFactory.CreateScope();
                        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();
                        return orderService.ProcessOrderPessmisticConcurenncyAsync(request, stoppingToken);
                    }).ToList();

                    await Task.WhenAll(tasks); 
                }

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
