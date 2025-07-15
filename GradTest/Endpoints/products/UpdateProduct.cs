using GradTest.Models;
using GradTest.Persistence;

namespace GradTest.Endpoints.products;

public static class UpdateProduct
{
    public static void MapUpdateProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/products/{id}", async (ApplicationDbContext context, ProductRequest req, Guid id) =>
        {
            Product? product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }
            
            product.Name = req.Name;
            product.Price = req.Price;
            product.Description = req.Description;
            product.StockQuantity = req.StockQuantity;
            
            await context.SaveChangesAsync();
            return Results.Ok();
        });
    }
}