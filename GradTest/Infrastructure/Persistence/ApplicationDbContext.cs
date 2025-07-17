using GradTest.Models;
using Microsoft.EntityFrameworkCore;
namespace GradTest.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
}