using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Domain.Entities;

public class OrderProduct
{
    [Key]
    [SwaggerSchema("The order product ID.")]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [SwaggerSchema("The ID of the product being ordered.", Nullable = false)]
    public required Guid ProductId { get; init; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    [SwaggerSchema("The number of units of the product in the order. Must be at least 1.", Nullable = false)]
    public int Quantity { get; init; }
    
    public OrderProduct(Guid productId, int quantity = 1)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderProduct() {}
}