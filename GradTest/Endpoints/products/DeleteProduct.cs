using GradTest.Models;
using GradTest.Persistence;

namespace GradTest.Endpoints.products;

public static class DeleteProduct
{
    public static void MapDeleteProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/products/{id}", async (ApplicationDbContext context, Guid id) =>
        {
            Product? product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}