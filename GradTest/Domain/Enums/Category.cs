using Ardalis.SmartEnum;

namespace GradTest.Domain.Enums;

public sealed class Category: SmartEnum<Category>
{
    public static readonly Category Unknown = new Category("Unknown", 0);
    public static readonly Category Electronics = new Category("Electronics",1);
    public static readonly Category Books = new Category("Book", 2);
    public static readonly Category Clothing = new Category("Clothing", 3);    
    public static readonly Category Merchandise = new Category("Merchandise", 4);
    
    private Category(string name, int value) : base(name, value) {}
}