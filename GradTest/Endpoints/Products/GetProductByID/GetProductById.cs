using GradTest.Models;
using GradTest.Persistence;

namespace GradTest.Endpoints.Products.GetProductByID;

public static class  GetProductById
{
    public static void MapGetProductById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/products/{id}", async (ApplicationDbContext context, Guid id) =>
            {
                var product = await context.Products.FindAsync(id);
                if (product is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(new GetProductByIdResponse(product));
            }).WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}