using DesignGuidelines.NamingConventions;

namespace DesignGuidelines.Members.Overloading
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public double TotalPrice { get; set; }

        List<Product> Products { get; set; }

        public void CalculateTotalPrice()
        {
            foreach (var product in Products)
            {
                TotalPrice += product.Price;
            }
        }

        public void CalculateTotalPrice(double discount)
        {
            // Shorter overloads should simply call through to a longer overload
            CalculateTotalPrice(discount, 0);
        }

        //  Ensure that parameters representing the same input have the same names across all overloads.
        public void CalculateTotalPrice(double discount, double shippingPrice)
        {
        }

        // DO use member overloading rather than defining members with default arguments.
        // CalculateTotalPrice(double discount) is a better approach than CalculateTotalPrice(double discount, double shippingPrice = 0)
        public void CalculateTotalPrice(double discount, double shippingPrice = 0) // Bad practice
        {
        }
    }

    public class Overloading
    {
    }
}
