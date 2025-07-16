using System.ComponentModel.DataAnnotations;
using GradTest.Models;
using GradTest.Persistence;
using GradTest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace GradTest.Endpoints.Products.CreateProduct;

public static class CreateProduct
{
    public static void MapCreateProduct(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/admin/products",
            async (HttpContext httpContext, ApplicationDbContext context, [FromBody] CreateProductRequest req) =>
            {
                await AuthenticationMiddleware.AdminAuthorize(httpContext);
                    
                if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    return Results.Unauthorized();
                } 
                
                var validationErrors = new List<ValidationResult>();
                
                var validationContext = new ValidationContext(req);

                if (!Validator.TryValidateObject(req, validationContext, validationErrors))
                {
                    var errors = validationErrors.ToDictionary(
                        r => r.MemberNames.FirstOrDefault() ?? "General",
                        r => new[] { r.ErrorMessage ?? "Invalid Value" });
                    
                    return Results.ValidationProblem(errors);
                }
                
                Product newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = req.Name,
                    CategoryValue = req.Category.Value,
                    Description = req.Description,
                    Price = req.Price,
                    StockQuantity = req.StockQuantity 
                };
                
                await context.Products.AddAsync(newProduct);
                
                await context.SaveChangesAsync();
                
                return Results.Created($"/product/{newProduct.Id}", new CreateProductResponse(newProduct));
            });
    } 
}