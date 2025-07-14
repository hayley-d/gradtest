namespace GradTest.Models;

public class ProductResponse
{
    Guid Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Category Category { get; set; }
    decimal Price { get; set; }
    int StockQuantity { get; set; }

    public ProductResponse(Product product)
    {
        this.Id = product.Id;
        
    }
}