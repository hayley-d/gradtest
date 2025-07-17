using GradTest.Domain.Entities;
namespace GradTest.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryResponse
{
    public Guid Id { get; init; }

    public string UserId { get; init; }

    public DateTime OrderDate { get; init; }

    public decimal ZarToUsd { get; init; }

    public List<OrderProduct> Products { get; init; } = new();

    public GetAllOrdersQueryResponse(Order order)
    {
        Id = order.Id;
        UserId = order.UserId;
        OrderDate = order.OrderDate;
        ZarToUsd = order.ZarToUsd;
        Products = order.Products;
    }
    
    public GetAllOrdersQueryResponse() {}
}