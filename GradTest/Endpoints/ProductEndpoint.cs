using GradTest.Models;

namespace GradTest.Endpoints;

public static class ProductEndpoint
{
    public static void GetPrducts(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/products", async (ApplicationDbContext context) =>
        {
            // TODO: Make get all products endpoint
        });
    }
}