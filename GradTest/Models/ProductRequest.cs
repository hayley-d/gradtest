namespace GradTest.Models;

public class ProductRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}