using Ardalis.SmartEnum;

namespace GradTest.Domain.Enums;

public sealed class Category: SmartEnum<Category>
{
    public static readonly Category Unknown = new Category("Unknown", 0);
    public static readonly Category Electronics = new Category("Electronics", 1);
    public static readonly Category Books = new Category("Books", 2);
    public static readonly Category Clothing = new Category("Clothing", 3);
    public static readonly Category Merchandise = new Category("Merchandise", 4); 

    /*private sealed class UnknownCategory() : Category("Unknown", 0)
    {
    }
    
    private sealed class ElectronicsCategory() : Category("Electronics", 1)
    {
        
    }
    
    private sealed class BooksCategory() : Category("Books", 2)
    {
        
    }
    
    private sealed class ClothingCategory() : Category("Clothing", 3)
    {
        
    }
    
    private sealed class MerchandiseCategory() : Category("Merchandise", 4)
    {
        
    }*/
    private Category(string name, int value) : base(name, value) {}
    

    /*public static IEnumerable<Category?> List()
    {
        return [Unknown, Electronics, Books, Clothing, Merchandise];
    }
    
    public static Category FromString(string categoryString)
    {
        return List().FirstOrDefault(category => String.Equals(category.Name, categoryString, StringComparison.OrdinalIgnoreCase));
    }*/
}