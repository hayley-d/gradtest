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
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "The witcher book", Name = "The witcher 1", Price = 99, StockQuantity = 7},
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "The witcher book 2", Name = "The witcher 2", Price = 99, StockQuantity = 7},
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "The witcher book 3", Name = "The witcher 3", Price = 99, StockQuantity = 7},
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "Lotr book 1", Name = "Lord of the Rings 1", Price = 99, StockQuantity = 7},
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "Lotr book 2", Name = "Lord of the Rings 2", Price = 99, StockQuantity = 7},
            new Product { Id = Guid.NewGuid(), Category = Category.Books, Description = "Lotr book 3", Name = "Lord of the Rings 3", Price = 99, StockQuantity = 7}
        );
    }
}