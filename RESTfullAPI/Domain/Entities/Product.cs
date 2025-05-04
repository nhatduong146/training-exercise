using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RESTfullAPI.Domain.Entities
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public Product() : base()
        {

        }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public List<Order> Orders { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
