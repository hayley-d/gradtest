using System.ComponentModel.DataAnnotations;
using GradTest.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Products.ListProducts;

public class ListProductRequest : IValidatableObject
{
        [SwaggerSchema("The value of the category enum.", Nullable = true)]
        [Range(0, 3, ErrorMessage = "CategoryValue must be between 0 and 3.")]
        public int? CategoryValue { get; init; }

        [SwaggerSchema("The minimum price for the products.", Nullable = true)]
        [Range(0, double.MaxValue, ErrorMessage = "MinPrice must be a positive number.")]
        public decimal? MinPrice { get; init; }

        [SwaggerSchema("The maximum price for the products.", Nullable = true)]
        [Range(0, double.MaxValue, ErrorMessage = "MaxPrice must be a positive number.")]
        public decimal? MaxPrice { get; init; }

        [SwaggerSchema("Current page offset (1-based).", Nullable = true)]
        [Range(1, int.MaxValue, ErrorMessage = "PageNumber must be at least 1.")]
        public int PageNumber { get; init; } = 1;

        [SwaggerSchema("Amount of items per page (1 to 100).", Nullable = true)]
        [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100.")]
        public int PageSize { get; init; } = 10;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
                if (MinPrice.HasValue && MaxPrice.HasValue && MinPrice > MaxPrice)
                {
                        yield return new ValidationResult(
                                "MinPrice cannot be greater than MaxPrice.",
                                new[] { nameof(MinPrice), nameof(MaxPrice) });
                }

                if (CategoryValue is not null && Category.FromValue(CategoryValue.Value) is null)
                {
                        yield return new ValidationResult(
                                $"Invalid category value: {CategoryValue}.",
                                new[] { nameof(CategoryValue) });
                }
        }

        public Category? GetCategory()
        {
                if (CategoryValue is not null)
                        return Category.FromValue(CategoryValue.Value);
                return null;
        }
}