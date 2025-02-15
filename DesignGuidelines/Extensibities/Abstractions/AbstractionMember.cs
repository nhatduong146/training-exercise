using DesignGuidelines.Members.Constructor;

namespace DesignGuidelines.Extensibities.Abstractions
{
    public abstract class Order
    {
        public string OrderCode { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        public List<Product> Products { get; set; }

        // Concrete method (common for all orders)
        public double CalculateTotalPrice()
        {
            return (double)Products.Sum(p => p.Price);
        }

        // Abstract method (must be implemented by derived classes)
        public abstract double CalculateShippingPrice();
    }

    public class LocalOrder : Order
    {
        public const string LocalOrderCode = "ABCC_12";

        public override double CalculateShippingPrice()
        {
            return 500000;
        }
    }

    public class AbstractionMember
    {
    }
}
