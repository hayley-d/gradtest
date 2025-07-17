using GradTest.API.Endpoints.Orders;
using GradTest.API.Endpoints.Products;

namespace GradTest.API.Endpoints;

public static class ApiEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductsEndpoints();
        app.MapOrdersEndpoints();
    }
}