namespace DesignGuidelines.NamingConventions
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        List<Product> Products { get; set; }

        // DO choose easily readable identifier names.
        public void CalculateTotalPrice() // Instead of Calc()
        {
            foreach (var product in Products)
            {
                TotalPrice += product.Price;
            }
        }

        public bool CheckValidOrder() // Instead of Check()
        {
            return Products.Count > 0;
        }

        // DO use a name similar to the old API when creating new versions of an existing API
        // DO prefer adding a suffix rather than a prefix to indicate a new version of an existing API
        public bool CheckValidOrderV2()
        {
            return TotalPrice > 0;
        }
    }

    // CONSIDER ending the name of derived classes with the name of the base class
    public class ArgumentOutOfRangeException : Exception
    {
        public ArgumentOutOfRangeException(string message) : base(message)
        {
        }
    }

    public class SerializableAttribute : Attribute
    {
        
    }
}
