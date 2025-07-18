using GradTest.Domain.Entities;
using GradTest.Domain.Enums;
using GradTest.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<OrderProduct>()
            .HasOne(p => p.Product)
            .WithMany() 
            .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<Product>()
            .Property(x => x.Category)
            .HasConversion<CategoryValueConverter>();
        
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = Guid.NewGuid(), Name = "The Silmarillion", Description = "A mythic history of Middle-earth by J.R.R. Tolkien.", Price = 299.99m, Category = Category.Books, StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "Blood of Elves", Description = "The first novel in the Witcher Saga by Andrzej Sapkowski.", Price = 199.99m, Category = Category.Books,  StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "The Two Towers", Description = "The second volume of The Lord of the Rings.", Price = 249.99m, Category = Category.Books, StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "Time of Contempt", Description = "Geralt continues his journey amidst political tension.", Price = 189.99m, Category = Category.Books, StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "The Hobbit", Description = "Bilbo’s adventure to the Lonely Mountain.", Price = 219.99m, Category = Category.Books, StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "Lady of the Lake", Description = "Final book in the Witcher Saga.", Price = 209,  Category = Category.Books, StockQuantity = 400 },
            new Product { Id = Guid.NewGuid(), Name = "The Return of the King", Description = "The epic conclusion of the War of the Ring.", Price = 269, Category = Category.Books, StockQuantity = 400},
            new Product { Id = Guid.NewGuid(), Name = "Season of Storms", Description = "A standalone Witcher novel.", Price = 189, Category = Category.Books, StockQuantity = 400 },
            new Product { Id = Guid.NewGuid(), Name = "Season of Storms t-shirt M" , Description = "T-shirt with the cover art for the Season of Storms book.", Price = 189, Category = Category.Clothing, StockQuantity = 400 },
            new Product { Id = Guid.NewGuid(), Name = "Blood of Elves t-shirt M" , Description = "T-shirt with the cover art for the Blood of Elves book.", Price = 189, Category = Category.Clothing, StockQuantity = 400 },
            new Product { Id = Guid.NewGuid(), Name = "The Two Towers t-shirt M" , Description = "T-shirt with the cover art for the The Two Towers book.", Price = 189, Category = Category.Clothing, StockQuantity = 400 }
        );
    }
}