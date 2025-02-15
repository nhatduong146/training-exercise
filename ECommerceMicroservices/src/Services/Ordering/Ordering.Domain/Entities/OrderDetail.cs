namespace Ordering.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}
