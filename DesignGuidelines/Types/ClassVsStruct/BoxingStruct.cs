namespace DesignGuidelines.Types.ClassVsStruct
{
    public interface ICartItem
    {
        decimal GetTotalPrice();
    }

    public struct CartItem : ICartItem // Causes Boxing
    {
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; }

        public CartItem(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalPrice() => Price * Quantity;
    }

    // AVOID defining a struct when it will have to be boxed frequently.
    // Performance degradation because it involves both heap allocations and garbage collection.
    public class BoxingStruct
    {
        public static void Main()
        {
            List<ICartItem> cart = new List<ICartItem>(); // Boxing occurs
            cart.Add(new CartItem("Laptop", 1000m, 2)); // Struct converted to object (heap allocation)

            Console.WriteLine($"Total: {cart[0].GetTotalPrice()}");
        }
    }
}
