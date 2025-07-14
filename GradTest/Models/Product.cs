namespace GradTest.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    
    public Product(ProductRequest req)
    {
        this.Id = Guid.NewGuid();
        this.Name = req.Name;
        this.Description = req.Description;
        this.Price = req.Price;
        this.StockQuantity = req.StockQuantity;
    }
}