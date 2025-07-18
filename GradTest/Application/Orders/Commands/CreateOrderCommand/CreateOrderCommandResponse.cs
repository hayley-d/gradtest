using GradTest.Domain.Entities;

namespace GradTest.Application.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommandResponse
{
    public Guid Id { get; init; }

    public string UserId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal ZarToUsd { get; init; }

    public List<OrderProduct> Products { get; init; } = [];

    public CreateOrderCommandResponse(Order order, List<OrderProduct> products)
    {
        Id = order.Id;
        UserId = order.UserId;
        OrderDate = order.OrderDate;
        ZarToUsd = order.ZarToUsd;
        Products = products;
    }
    
    public CreateOrderCommandResponse() {}
}