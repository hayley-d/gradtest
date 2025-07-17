using GradTest.Domain.Entities;
using GradTest.Persistence;
using GradTest.Utils;

namespace GradTest.Endpoints.Products.DeleteProduct;

public static class DeleteProduct
{
    public static void MapDeleteProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("/admin/products/{id}", async (HttpContext httpContext, ApplicationDbContext context, Guid id) =>
        {
            await AuthenticationMiddleware.AdminAuthorize(httpContext);
                    
            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            } 
            
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