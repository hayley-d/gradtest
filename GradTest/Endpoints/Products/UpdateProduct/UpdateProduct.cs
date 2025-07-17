using GradTest.Domain.Entities;
using GradTest.Models;
using GradTest.Persistence;
using GradTest.Utils;

namespace GradTest.Endpoints.Products.UpdateProduct;

public static class UpdateProduct
{
    public static void MapUpdateProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("/products/{id}", async (HttpContext httpContext, ApplicationDbContext context, UpdateProductRequest req, Guid id) =>
        {
            await AuthenticationMiddleware.UserAuthorize(httpContext);
                    
            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            } 
            
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