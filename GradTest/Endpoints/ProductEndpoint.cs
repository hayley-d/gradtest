using GradTest.Models;
using GradTest.Persistence;

namespace GradTest.Endpoints;

public static class ProductEndpoint
{
    public static void GetProducts(this IEndpointRouteBuilder builder)
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
            async (ApplicationDbContext context, ProductRequest req) =>
            {
                Product newProduct = new Product(req);
                await context.Products.AddAsync(newProduct);
                await context.SaveChangesAsync();
                return Results.Created($"/product/{newProduct.Id}", new ProductResponse(newProduct));
            });
    }

    // Updates the product using the product ID and DTO
    public static void UpdateProduct(this IEndpointRouteBuilder builder)
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

    // Removes a product of the specified ID
    public static void DeleteProduct(this IEndpointRouteBuilder builder)
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