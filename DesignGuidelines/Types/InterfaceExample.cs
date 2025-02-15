namespace DesignGuidelines.Types
{
    public class PaymentProcessor
    {
    }

    public class ShippingProcessor
    {
    }

    // Multiple class inheritance is not allowed.
    public class OrderProcessor: PaymentProcessor, ShippingProcessor // Compile-time error
    {
    }

    public interface IPaymentProcessor
    {
    }

    public interface IShippingProcessor
    {
    }

    // Interface allows types to implement multiple interfaces
    public interface IOrderProcessor : IPaymentProcessor, IShippingProcessor 
    {
        
    }
    public class NewOrderProcessor : IOrderProcessor
    {
    }

    public class InterfaceExample
    {
        public static void Main()
        {
            // Not allow initializing an interface
            IOrderProcessor orderProcessor = new IOrderProcessor(); // Compile-time error

            // Allowed (instantiating a class that implements the interface)
            IOrderProcessor newOrderProcessor = new NewOrderProcessor();
        }
    }
}
