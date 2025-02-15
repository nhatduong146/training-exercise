namespace Ordering.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string PictureFileName { get; set; }

        public string PictureUri { get; set; }

        public int CategoryId { get; set; }

        public int ShopId { get; set; }
    }
}
