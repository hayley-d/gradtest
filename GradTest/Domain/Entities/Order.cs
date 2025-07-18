using System.ComponentModel.DataAnnotations;

namespace GradTest.Domain.Entities;

public class Order
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [MaxLength(250)]
    public required string UserId { get; init; } 
    
    [Required]
    public DateTime OrderDate { get; init; } = DateTime.UtcNow;
    
    [Required]
    [MinLength(1)]
    public List<OrderProduct> Products { get; init; } = [];
    
    [Required]
    [Range(0.0001, double.MaxValue)]
    public required decimal ZarToUsd { get; init; }

    public Order(string userId, List<OrderProduct> products, decimal zar)
    {
        UserId = userId;
        Products = products;
        ZarToUsd = zar;
    }
    
    public Order() {}
}