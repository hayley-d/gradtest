using GradTest.Endpoints.Orders.CreateOrder;
using GradTest.Endpoints.Orders.GetAllOrders;
using GradTest.Endpoints.Orders.GetOrderByID;
using GradTest.Endpoints.Orders.GetOrdersByUser;

namespace GradTest.Endpoints.Orders;

public static class MapOrderEndpoints
{
    public static void MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateOrder();
        app.MapGetOrdersByUser();
        app.MapGetOrderById();
        app.MapGetAllOrders();
    }
}