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
            async (ApplicationDbContext context, string? category = null, decimal? minPrice = null,
                decimal? maxPrice = null, int pageNumber=0, int pageSize=10) =>
            {
                if(pageNumber < 1)
                    pageNumber = 1;

                if (pageSize < 1 || pageSize > 100)
                {
                    pageSize = 10;
                }
                
                var query = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    var categoryEnum = Category.List.FirstOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
                    if (categoryEnum is not null)
                    {
                        query = query.Where(c => c.Category == categoryEnum);
                    }
                }
                if (minPrice is not null) 
                    query = query.Where(c => c.Price >= minPrice);
                
                if (maxPrice is not null)
                    query = query.Where(c => c.Price <= maxPrice);
                
                int totalCount = await query.CountAsync();
                int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
                
                var items = await query.OrderBy(p => p.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(p => new ProductResponse(p)).ToListAsync();

                PagedResponse<ProductResponse> results = new PagedResponse<ProductResponse>(items, totalCount, pageSize, pageNumber, totalPages);
                
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