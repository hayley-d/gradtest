using GradTest.Domain.Entities;
using GradTest.Domain.Enums;
namespace GradTest.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommandResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Category { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }

    public CreateProductCommandResponse(Guid id, string name, string description, Category category, decimal price,
        int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category.Value;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public CreateProductCommandResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Category = product.Category.Value;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
    }
}