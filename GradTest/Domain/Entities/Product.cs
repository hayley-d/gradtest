using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradTest.Domain.Enums;

namespace GradTest.Domain.Entities;

public class Product
{
    [Required]
    public Guid Id { get; init; }
    
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    
    [Required]
    [StringLength(350)]
    public required string Description { get; set; }
    
    [Required]
    [Range(1, 3)]
    public int CategoryValue { get; set; }
    
    [Required]
    [Range(0, Double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0, Int32.MaxValue)]
    public int StockQuantity { get; set; }
    
    [NotMapped]
    public Category Category
    {
        get => Category.FromValue(CategoryValue);
        set => CategoryValue = value.Value;
    }

    public Product()
    {
        Id = Guid.NewGuid();
    }
}