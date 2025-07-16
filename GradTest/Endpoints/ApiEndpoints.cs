using GradTest.Endpoints.Products;

namespace GradTest.Endpoints;

public static class ApiEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapProductsEndpoints();
    }
}