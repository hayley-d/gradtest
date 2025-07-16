using GradTest.Models;
using GradTest.Persistence;
using GradTest.Utils;

namespace GradTest.Endpoints.Products.GetProductByID;

public static class  GetProductById
{
    public static void MapGetProductById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/products/{id}", async (HttpContext httpContext, ApplicationDbContext context, Guid id) =>
        {
            await AuthenticationMiddleware.UserAuthorize(httpContext);

            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            }

            var product = await context.Products.FindAsync(id);

            if (product is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new GetProductByIdResponse(product));
        });
    }
}