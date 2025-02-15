namespace DesignGuidelines.Members.Constructor
{
    /*
     * CONSIDER using a static factory method instead of a constructor 
     * if the semantics of the desired operation do not map directly 
     * to the construction of a new instance
     */
    public class Product
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        // Private constructor to prevent direct instantiation
        private Product(string name, decimal price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        // Static factory method
        public static Product CreateProduct(string name, decimal price, int quantity)
        {
            if (price <= 0) throw new ArgumentException("Price must be positive");
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative");

            return new Product(name, price, quantity);
        }

        // Alternatively, another factory method with different semantics
        public static Product CreateDiscountedProduct(string name, decimal price, int quantity, decimal discount)
        {
            decimal discountedPrice = price - (price * discount);
            return new Product(name, discountedPrice, quantity);
        }
    }

    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        public Order()
        {
        }

        /*
         * DO use constructor parameters as shortcuts for setting main properties.
         * Avoids the need for repetitive property assignment after object creation
         */
        public Order(string status, double totalPrice)
        {
            // DO throw exceptions from instance constructors, if appropriate.
            if (totalPrice <= 0) throw new ArgumentException("Total price must be positive");

            Status = status;
            TotalPrice = totalPrice;
        }
    }

    public class Constructor
    {
        public static void Main()
        {
            // Use the static factory method to create a new instance
            var product = Product.CreateProduct("Laptop", 1000.00m, 5);

            // Use the constructor to create a new instance
            var order = new Order("Pending", 500.00);

            // Bad practice: Repetitive property assignment
            var order2 = new Order()
            {
                Id = 1,
                TotalPrice = 500.00
            };
        }
    }
}
