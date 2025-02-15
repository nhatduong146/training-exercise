namespace DesignGuidelines.Types.AbstractClass
{
    public abstract class PaymentProcessor
    {
        /*
         * DO define a protected constructor in abstract classes
         * Abstract classes should only be instantiated through derived classes, so their constructors must be protected.
         */
        protected PaymentProcessor() // Protected constructor (correct)
        {
            Console.WriteLine("Initializing Payment Processor");
        }

        /*
         * AVOID defining a public constructor in abstract classes
         */
        public PaymentProcessor(string method)
        {
            Console.WriteLine("Initializing Payment Processor with method");
        }
    }

    public class CreditCardProcessor : PaymentProcessor
    {
        public CreditCardProcessor() : base() // Call base constructor
        {
        }

        public CreditCardProcessor(string method)
        {
            Console.WriteLine("Credit Card Processor Initialized with method");
        }
    }

    public class AbstractClass
    {
        public static void Main()
        {
            // Compile-time error: cannot create an instance of an abstract class
             PaymentProcessor paymentProcessor = new PaymentProcessor();

            // Allowed (instantiating derived class)
            PaymentProcessor creditCardProcessor = new CreditCardProcessor();
        }
    }
}
