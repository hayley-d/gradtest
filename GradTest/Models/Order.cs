using System.ComponentModel.DataAnnotations;

namespace GradTest.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string UserId { get; set; } 
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public List<OrderProduct> Products { get; set; } = new();

    public decimal GetCurrentExchangeRate()
    {
        return 1;
    }
}