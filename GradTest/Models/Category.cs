namespace GradTest.Models;
using Ardalis.SmartEnum;

public sealed class Category: SmartEnum<Category>
{
    public static readonly Category FOOD = new Category("FOOD",1, 1000, true);
    
    public decimal Price { get; private set; }
    public bool InStock { get; private set; }

    private Category(string name, int value, decimal price, bool inStock) : base(name, value)
    {
        this.Price = price;
        this.InStock = inStock;
    }

    
}