using System.ComponentModel.DataAnnotations;
using GradTest.Models;
using GradTest.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GradTest.Endpoints.products;

public static class CreateProduct
{
    public static void MapCreateProduct(this IEndpointRouteBuilder builder)
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
                return Results.Created($"/product/{newProduct.Id}", new CreateProductResponse(newProduct));
            });
    } 
}