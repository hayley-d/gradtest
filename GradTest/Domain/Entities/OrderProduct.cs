using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Domain.Entities;

public class OrderProduct
{
    [Key]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required] 
    public Guid OrderId { get; init; }

    [Required]
    public Guid ProductId { get; init; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; init; }
    
    public Order? Order { get; init; }
    public Product? Product { get; init; }
    
    public OrderProduct(Guid orderId, Guid productId, int quantity = 1)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderProduct() {}
}