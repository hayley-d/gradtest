using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GradTest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Orders.CreateOrder;

public class CreateOrderRequest : IValidatableObject
{
    [SwaggerSchema("The list of products to order, with quantity.", Nullable = false)]
    [Required]
    public required List<OrderProductRequest> Products { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Products is null || Products.Count == 0)
        {
            yield return new ValidationResult(
                "At least one product must be included in the order.",
                new[] { nameof(Products) });
        }

        if (Products is not null)
        {
            var duplicateIds = Products
                .GroupBy(p => p.ProductId)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateIds.Any())
            {
                yield return new ValidationResult(
                    "Duplicate product IDs are not allowed in the order.",
                    new[] { nameof(Products) });
            }
            
            foreach (var product in Products)
            {
                if (product.Quantity <= 0)
                {
                    yield return new ValidationResult(
                        $"Product {product.ProductId} must have a quantity greater than 0.",
                        new[] { nameof(Products) });
                }
            }
        }

        
    }
}

public class OrderProductRequest
{
        [Required]
        [SwaggerSchema("The ID of the product being ordered.", Nullable = false)]
        public required Guid ProductId { get; init; }
    
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        [SwaggerSchema("The number of units of the product in the order. Must be at least 1.", Nullable = false)]
        public int Quantity { get; init; }
    
        public OrderProductRequest(Guid productId, int quantity = 1)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        public OrderProductRequest() {}

        public OrderProduct Convert()
        {
            return new OrderProduct {
                ProductId = ProductId, 
                Quantity =  Quantity,
            };
        }
}