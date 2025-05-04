namespace ConcurrencyLab.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public string? FailureReason { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }
    }
}
