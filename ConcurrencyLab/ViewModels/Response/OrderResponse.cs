namespace ConcurrencyLab.ViewModels.Response
{
    public class OrderResponse
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string? FailureReason { get; set; }
    }
}
