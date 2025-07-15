using GradTest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.products.GetProductByID;

public class GetProductByIdResponse
{
        [SwaggerSchema("The unique identifier of the product.")]
        public Guid Id { get; init; }
        [SwaggerSchema("The name of the product.")]
        public string Name { get; init; }
        [SwaggerSchema("The description of the product.")]
        public string Description { get; init; }
        [SwaggerSchema("The category of the product.")]
        public int Category { get; init; }
        [SwaggerSchema("The price of the product in Rands.")]
        public decimal Price { get; init; }
        [SwaggerSchema("The current stock quantity of the product.")]
        public int StockQuantity { get; init; }

        public GetProductByIdResponse(Guid id, string name, string description, Category category, decimal price,
            int stockQuantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category.Value;
            Price = price;
            StockQuantity = stockQuantity;
        }

        public GetProductByIdResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Category = product.Category.Value;
            Price = product.Price;
            StockQuantity = product.StockQuantity;
        }
}