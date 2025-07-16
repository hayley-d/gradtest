using System.Text.Json;
using GradTest.Models;
using GradTest.Persistence;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Products.ListProducts;

public static class ListProducts
{
    public static void MapListProducts(this IEndpointRouteBuilder builder)
    {
            builder.MapGet("/products",
                async (HttpContext httpContext, ApplicationDbContext context, [AsParameters] ListProductRequest query) =>
                {
                    bool authorized = false;
                    var roleClaim = httpContext.User.FindFirst("realm_access")?.Value;
                    if (roleClaim is not null)
                    {
                        var rolesObject = JsonDocument.Parse(roleClaim);
                        var rolesArray = rolesObject.RootElement.GetProperty("roles");
                        
                        foreach (var role in rolesArray.EnumerateArray())
                        {
                            var roleName = role.GetProperty("name").GetString();
                            if (roleName is not null && roleName.Equals("grad-admin"))
                            {
                                authorized = true;
                            }
                        }
                    }

                    if (!authorized)
                    {
                        return Results.Unauthorized();
                    }
                    //var value = httpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                    
                    int validatedPageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
                    int validatedPageSize = (query.PageSize < 1 || query.PageSize > 100) ? 10 : query.PageSize;

                    var category = query.GetCategory();
                    var productsQuery = context.Products.AsQueryable();

                    if (category is not null)
                        productsQuery = productsQuery.Where(p => p.CategoryValue == category.Value);

                    if (query.MinPrice.HasValue)
                        productsQuery = productsQuery.Where(p => p.Price >= query.MinPrice.Value);

                    if (query.MaxPrice.HasValue)
                        productsQuery = productsQuery.Where(p => p.Price <= query.MaxPrice.Value);
                
                    int totalCount = await productsQuery.CountAsync();
                
                    var items = await productsQuery.OrderBy(p => p.Name).Skip((validatedPageNumber - 1) * validatedPageSize).Take(validatedPageSize).Select(p => new ListProductsProductResponse(p)).ToListAsync();

                    ListProductsResponse<ListProductsProductResponse> results = new ListProductsResponse<ListProductsProductResponse>(items,new ListProductsPageMetadata(totalCount, validatedPageSize, validatedPageNumber));
                
                    return Results.Ok(results);
                });
        } 
}