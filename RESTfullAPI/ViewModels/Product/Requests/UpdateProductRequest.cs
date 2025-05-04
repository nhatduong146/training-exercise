using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RESTfullAPI.ViewModels.Product.Requests;

public class UpdateProductRequest : IValidatableObject
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Quantity { get; set; }

    //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    //{
    //    var validationResults = new List<ValidationResult>();

    //    if (!Regex.IsMatch(Name, @"^[a-zA-Z0-9\s]*$"))
    //        validationResults.Add(new ValidationResult("Name can only contain letters, numbers, and spaces.", [nameof(Name)]));

    //    if (Price <= 0)
    //        validationResults.Add(new ValidationResult("Price must be greater than zero.", [nameof(Price)]));

    //    if (Quantity < 0)
    //        validationResults.Add(new ValidationResult("Quantity cannot be negative.", [nameof(Quantity)]));

    //    return validationResults;
    //}

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Regex.IsMatch(Name, @"^[a-zA-Z0-9\s]*$"))
            yield return new ValidationResult("Name can only contain letters, numbers, and spaces.", [nameof(Name)]);

        if (Price <= 0)
            yield return new ValidationResult("Price must be greater than zero.", [nameof(Price)]);

        if (Quantity < 0)
            yield return new ValidationResult("Quantity cannot be negative.", [nameof(Quantity)]);
    }
}
