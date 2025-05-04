using System.ComponentModel.DataAnnotations;

namespace RESTfullAPI.ViewModels.Product.Requests;

public class CreateProductRequest
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
}
