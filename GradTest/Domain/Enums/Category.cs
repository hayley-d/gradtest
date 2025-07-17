namespace GradTest.Models;
using Ardalis.SmartEnum;

public sealed class Category: SmartEnum<Category>
{
    public static readonly Category UNKNOWN = new Category("UNKNOWN", 0, 0, false);
    public static readonly Category ELECTRONICS = new Category("ELECTRONICS",1, 1000, true);
    public static readonly Category BOOKS = new Category("BOOK", 2, 1000, true);
    public static readonly Category CLOTHING = new Category("CLOTHING", 3, 1000, true);    
    
    public decimal Price { get; private set; }
    public bool InStock { get; private set; }

    private Category(string name, int value, decimal price, bool inStock) : base(name, value)
    {
        this.Price = price;
        this.InStock = inStock;
    }
}