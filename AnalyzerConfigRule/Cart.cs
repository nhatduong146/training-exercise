namespace AnalyzerConfigRule
{
    public class Cart
    {
        public void ProcessTransaction()
        {
            // Method logic here
        }

        public int Calculate()
        {
            var transactionId = 1;
            var userId = 1;

            return transactionId + userId;
        }
    }

    public enum PaymentType
    {
        CreditCard = 1,
        DebitCard = 2,
        PayPal = 3
    }
}
