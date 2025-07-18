using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradTest.Domain.Enums;

namespace GradTest.Domain.Entities;

public class Product
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }
    
    [Required]
    [StringLength(350)]
    public required string Description { get; set; }
    
    [Required]
    [Range(0,double.MaxValue)]
    public decimal Price { get; set; }
    
    [Required]
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
    
    [Required]
    public Category Category  { get; set; }

    public Product(string name, string description, decimal price, int stockQuantity, Category category)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        Category = category;
    }

    public Product()
    {
        Id = Guid.NewGuid();
    }
}