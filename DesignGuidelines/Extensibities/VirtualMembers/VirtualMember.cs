namespace DesignGuidelines.Extensibities.VirtualMembers
{
    public class Order
    {
        // By default, methods are non-virtual. We can't override a non-virtual method.
        public void CalculateTotalPrice()
        {
        }

        // The virtual keyword is used to indicate that a method or property can be overridden in a derived class
        public virtual void CalculateShippingPrice()
        {
        }
    }

    public class LocalOrder : Order
    {
        public override void CalculateTotalPrice() // Compile-time error
        {
        }

        public override void CalculateShippingPrice() // Allowed
        {
        }
    }

    public class VirtualMember
    {
    }
}
