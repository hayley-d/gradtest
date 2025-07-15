using System.ComponentModel.DataAnnotations;
using GradTest.Models;
using GradTest.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints;

public static class ProductEndpoint
{
    public static void GetProductById(this IEndpointRouteBuilder builder)
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

    public static void ListProducts(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/products",
            async (ApplicationDbContext context, [AsParameters] ProductQueryRequest query) =>
            {
                int validatedPageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
                int validatedPageSize = (query.PageSize < 1 || query.PageSize > 100) ? 10 : query.PageSize;

                //var query = context.Products.AsQueryable();
                var category = query.GetCategory();
                var productsQuery = context.Products.AsQueryable();

                if (category is not null)
                    productsQuery = productsQuery.Where(p => p.Category == category);

                if (query.MinPrice.HasValue)
                    productsQuery = productsQuery.Where(p => p.Price >= query.MinPrice.Value);

                if (query.MaxPrice.HasValue)
                    productsQuery = productsQuery.Where(p => p.Price <= query.MaxPrice.Value);
                
                int totalCount = await productsQuery.CountAsync();
                
                var items = await productsQuery.OrderBy(p => p.Name).Skip((validatedPageNumber - 1) * validatedPageSize).Take(validatedPageSize).Select(p => new ProductResponse(p)).ToListAsync();

                PagedResponse<ProductResponse> results = new PagedResponse<ProductResponse>(items,new PageMetadata(totalCount, validatedPageSize, validatedPageNumber));
                
                return Results.Ok(results);
            });
    }

    // Creates a new product
    public static void CreateProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/products",
            async (ApplicationDbContext context, [FromBody] ProductRequest req) =>
            {
                var validationErrors = new List<ValidationResult>();
                var validationContext = new ValidationContext(req);

                if (!Validator.TryValidateObject(req, validationContext, validationErrors))
                {
                    var errors = validationErrors.ToDictionary(
                        r => r.MemberNames.FirstOrDefault() ?? "General",
                        r => new[] { r.ErrorMessage ?? "Invalid Value" });
                    
                    return Results.ValidationProblem(errors);
                }
                
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