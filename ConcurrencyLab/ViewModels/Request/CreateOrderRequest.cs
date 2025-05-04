namespace ConcurrencyLab.ViewModels.Request
{
    public class CreateOrderRequest
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
