using System.ComponentModel.DataAnnotations;

namespace GradTest.Models;

public class OrderProduct
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    public Product? Product { get; set; }

    public OrderProduct(Guid productId, int quantity = 1)
    {
        
    }
}