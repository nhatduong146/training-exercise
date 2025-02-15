namespace DesignGuidelines.Members.Property
{
    /*
     *  DO create get-only properties if the caller should not be able to change the value of the property.
     */
    public class PaymentTransaction
    {
        // Get-only properties
        public string TransactionId { get; }

        public decimal Amount { get; }

        // DO provide sensible default values for all properties
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        /*
         * DO NOT provide set-only properties or properties with the setter having broader accessibility than the getter.
         * Bad practice: Public setter with protected getter
         */
        public string Status {protected get; set; }

        public PaymentTransaction(string transactionId, decimal amount)
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }

    public class Property
    {
        public static void Main()
        {
            var transaction = new PaymentTransaction("123456", 100.00m);
            transaction.TransactionId = "654321"; // Compile-time error
        }
    }
}
