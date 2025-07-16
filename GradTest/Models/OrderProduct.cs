using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Models;

public class OrderProduct
{
    [Key]
    [SwaggerSchema("The unique identifier for the order-product mapping.")]
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [Required]
    [SwaggerSchema("The ID of the product being ordered.", Nullable = false)]
    public required Guid ProductId { get; init; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    [SwaggerSchema("The number of units of the product in the order. Must be at least 1.", Nullable = false)]
    public int Quantity { get; init; }
    
    [Required]
    [SwaggerSchema("The actual product entity reference.", ReadOnly = true)]
    public required Product Product { get; init; }

    public OrderProduct(Guid productId, Product product, int quantity = 1)
    {
        ProductId = productId;
        Quantity = quantity;
        Product = product;
    }
    
    public OrderProduct() {}
}