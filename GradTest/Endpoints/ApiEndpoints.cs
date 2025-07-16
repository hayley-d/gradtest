using GradTest.Endpoints.Products;
using GradTest.Endpoints.Orders;
namespace GradTest.Endpoints;

public static class ApiEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductsEndpoints();
        app.MapOrdersEndpoints();
    }
}