using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradTest.Models;

public class Product
{
    public Guid Id { get; init; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(350)]
    public string Description { get; set; }
    [Required]
    [Range(0, 3)]
    public int CategoryValue { get; set; }
    
    [NotMapped]
    public Category Category
    {
        get => Models.Category.FromValue(CategoryValue);
        set => CategoryValue = value.Value;
    }
    [Required]
    [Range(0, Double.MaxValue)]
    public decimal Price { get; set; }
    [Required]
    [Range(0, Int32.MaxValue)]
    public int StockQuantity { get; set; }
    
    public Product() {}
    
    public Product(ProductRequest req)
    {
        this.Id = Guid.NewGuid();
        this.Name = req.Name;
        this.CategoryValue = req.Category;
        this.Description = req.Description;
        this.Price = req.Price;
        this.StockQuantity = req.StockQuantity;
    }
}