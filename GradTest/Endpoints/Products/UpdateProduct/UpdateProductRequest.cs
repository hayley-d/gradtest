using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GradTest.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Products.UpdateProduct;

public class UpdateProductRequest
{
    [SwaggerSchema("The name of the product.", Nullable = false)]
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [SwaggerSchema("The description of the product.", Nullable = false)]
    [Required]
    [StringLength(350)]
    public string Description { get; set; }
    [Required]
    [Range(0, 3, ErrorMessage = "CategoryValue must be between 0 and 3.")]
    public int CategoryValue { get; set; }
    [SwaggerSchema("The price (in Rands) must be positive.", Nullable = false)]
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
    public required decimal Price { get; set; }
    [SwaggerSchema("The stock quantity must be positive.", Nullable = false)]
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "StockQuantity must be a positive number.")]
    public required int StockQuantity { get; set; }
    
    [JsonIgnore]
    [SwaggerSchema(ReadOnly = true)]
    public Category Category => Category.FromValue(CategoryValue);
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Category.FromValue(CategoryValue) is null)
        {
            yield return new ValidationResult(
                $"Invalid category value: {CategoryValue}.",
                new[] { nameof(CategoryValue) });
        }
    }
}