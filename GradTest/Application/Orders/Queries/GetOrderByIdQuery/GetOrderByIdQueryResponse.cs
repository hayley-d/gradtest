using GradTest.Domain.Entities;

namespace GradTest.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryResponse
{
    public Guid Id { get; init; }

    public string UserId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal ZarToUsd { get; init; }

    public List<OrderProduct> Products { get; init; } = new();

    public GetOrderByIdQueryResponse(Order order)
    {
        Id = order.Id;
        UserId = order.UserId;
        OrderDate = order.OrderDate;
        ZarToUsd = order.ZarToUsd;
        Products = order.Products;
    }
    
    public GetOrderByIdQueryResponse() {}
} 