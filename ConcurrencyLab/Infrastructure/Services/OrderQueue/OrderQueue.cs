using ConcurrencyLab.ViewModels.Request;
using System.Collections.Concurrent;

namespace ConcurrencyLab.Infrastructure.Services.OrderQueue
{
    public class OrderQueue
    {
        private readonly ConcurrentQueue<CreateOrderRequest> _orderQueue = new();

        public void Enqueue(CreateOrderRequest request)
        {
            _orderQueue.Enqueue(request);
        }

        public List<CreateOrderRequest> DequeueBatch(int batchSize)
        {
            var batch = new List<CreateOrderRequest>();

            while (batch.Count < batchSize && _orderQueue.TryDequeue(out var request))
            {
                batch.Add(request);
            }

            return batch;
        }

        public int Count => _orderQueue.Count;
    }
}
