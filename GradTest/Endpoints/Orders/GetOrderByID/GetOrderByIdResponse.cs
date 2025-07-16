using System.ComponentModel.DataAnnotations;
using GradTest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Orders.GetOrderByID;

public class GetOrderByIdResponse
{
        [SwaggerSchema("The unique identifier of the created order.")]
        [Required]
        public Guid Id { get; init; }

        [SwaggerSchema("The ID of the user who placed the order.")]
        [MaxLength(250)]
        [Required]
        public string UserId { get; init; }

        [SwaggerSchema("The UTC timestamp of when the order was placed.")]
        [Required]
        public DateTime OrderDate { get; init; }

        [SwaggerSchema("The exchange rate (ZAR to USD) at the time of order.")]
        [Range(0.0001, double.MaxValue)]
        [Required]
        public decimal ZarToUsd { get; init; }

        [SwaggerSchema("The list of ordered products.")]
        [Required]
        public List<OrderProduct> Products { get; init; } = new();

        public GetOrderByIdResponse(Order order)
        {
            Id = order.Id;
            UserId = order.UserId;
            OrderDate = order.OrderDate;
            ZarToUsd = order.ZarToUsd;
            Products = order.Products;
        }
    
        public GetOrderByIdResponse() {}
} 