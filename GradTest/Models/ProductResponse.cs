namespace GradTest.Models;

public class ProductResponse
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Category Category { get; set; }
    decimal Price { get; set; }
    int StockQuantity { get; set; }

    public ProductResponse(Guid id, string name, string description, Category category, decimal price,
        int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public ProductResponse(Product product)
    {
        this.Id = product.Id;
        this.Name = product.Name;
        this.Description = product.Description;
        this.Category = product.Category;
        this.Price = product.Price;
        this.StockQuantity = product.StockQuantity;
    }
}