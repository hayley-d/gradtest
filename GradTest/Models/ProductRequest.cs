using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Models;

public class ProductRequest
{
    [SwaggerSchema("The name of the product.", Nullable = false)]
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    [SwaggerSchema("The description of the product.", Nullable = false)]
    [Required]
    [StringLength(350)]
    public required string Description { get; set; }
    [Required]
    [Range(0, 3)]
    public required int CategoryValue { get; set; }
    [SwaggerSchema("The price (in Rands) must be positive.", Nullable = false)]
    [Required]
    [Range(0, Double.MaxValue)]
    public required decimal Price { get; set; }
    [SwaggerSchema("The stock quantity must be positive.", Nullable = false)]
    [Required]
    [Range(0, Int32.MaxValue)]
    public required int StockQuantity { get; set; }
    
    // Derived SmartEnum from enum value
    public Category Category => Category.FromValue(CategoryValue);
}