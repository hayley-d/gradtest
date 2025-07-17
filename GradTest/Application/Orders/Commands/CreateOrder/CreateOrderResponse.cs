using System.ComponentModel.DataAnnotations;
using GradTest.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Application.Orders.Commands.CreateOrder;

public class CreateOrderResponse
{
    public Guid Id { get; init; }

    public string UserId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal ZarToUsd { get; init; }

    public List<OrderProduct> Products { get; init; } = new();

    public CreateOrderResponse(Order order)
    {
        Id = order.Id;
        UserId = order.UserId;
        OrderDate = order.OrderDate;
        ZarToUsd = order.ZarToUsd;
        Products = order.Products;
    }
    
    public CreateOrderResponse() {}
}