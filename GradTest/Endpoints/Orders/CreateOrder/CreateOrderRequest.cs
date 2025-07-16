using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GradTest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Orders.CreateOrder;

public class CreateOrderRequest : IValidatableObject
{
    [SwaggerSchema("The list of products to order, with quantity.", Nullable = false)]
    [Required]
    public required List<OrderProduct> Products { get; init; }

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