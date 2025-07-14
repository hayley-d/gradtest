using GradTest.Models;
using GradTest.Persistence;

namespace GradTest.Endpoints;

public static class ProductEndpoint
{
    public static void GetPrducts(this IEndpointRouteBuilder builder)
    {
        // Gets a single product using the product ID
        builder.MapGet("/products/{id}", async (ApplicationDbContext context, Guid id) =>
        {
            var product = await context.Products.FindAsync(id);
            if (product is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(new ProductResponse(product));
        });
    }

    // Creates a new product
    public static void CreateProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/product",
            async (ApplicationDbContext ApplicationDbContext, HttpContext httpContext, ProductRequest product) =>
            {
                Product newProduct = new Product(product);
                await ApplicationDbContext.Products.AddAsync(newProduct);
                await ApplicationDbContext.SaveChangesAsync();
                return Results.Created($"/product/{newProduct.Id}", newProduct);
            });
    }
}