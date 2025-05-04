using System.Collections.Concurrent;

namespace ConcurrencyLab.ConsoleApp
{
    public static class OrderConcurrencyBag
    {
        public async static void ShowOrderList()
        {
            var orders = new ConcurrentBag<string>();
            Task t1 = Task.Run(() => GetOrders("Customer A", orders));
            Task t2 = Task.Run(() => GetOrders("Customer B", orders));
            await Task.WhenAll(t1, t2);

            foreach (var order in orders)
            {
                Console.WriteLine($"Order Placed: {order}");
            }
        }
        public static void GetOrders(string custName, ConcurrentBag<string> orders)
        {
            for (int i = 1; i <= 5; i++)
            {
                string order = string.Format($"{custName} buy {i}");
                orders.Add(order);
            }
        }
    }
}
