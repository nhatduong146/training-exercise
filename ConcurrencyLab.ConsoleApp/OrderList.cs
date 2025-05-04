namespace ConcurrencyLab.ConsoleApp
{
    public static class OrderList
    {
        static object lockObject = new object();

        public async static void ShowOrderList()
        {
            var orders = new List<string>();
            Task t1 = Task.Run(() => CreateOrders("Customer A", orders));
            Task t2 = Task.Run(() => CreateOrders("Customer B", orders));
            await Task.WhenAll(t1, t2);

            foreach (var order in orders)
            {
                Console.WriteLine($"Order Placed: {order}");
            }
        }
        public static void CreateOrders(string custName, List<string> orders)
        {
            for (int i = 1; i <= 5; i++)
            {
                string order = string.Format($"{custName} buy {i}");
                orders.Add(order);
            }
        }

        public static void CreateOrdersWithLock(string custName, List<string> orders)
        {
            for (int i = 1; i <= 5; i++)
            {
                string order = string.Format($"{custName} buy {i}");
                lock(lockObject)
                {
                    orders.Add(order);
                }
            }
        }
    }
}
