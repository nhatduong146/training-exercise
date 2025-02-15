namespace Ordering.Domain.Entities
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public string Address { get; set; }

        public string PaymentMethod { get; set; }

        public int UserId { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = [];
    }
}
