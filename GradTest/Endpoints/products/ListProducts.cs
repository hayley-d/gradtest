using GradTest.Models;
using GradTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.products;

public static class ListProducts
{
    public static void MapListProducts(this IEndpointRouteBuilder builder)
    {
            builder.MapGet("/products",
                async (ApplicationDbContext context, [AsParameters] ProductQueryRequest query) =>
                {
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
                
                    var items = await productsQuery.OrderBy(p => p.Name).Skip((validatedPageNumber - 1) * validatedPageSize).Take(validatedPageSize).Select(p => new ProductResponse(p)).ToListAsync();

                    PagedResponse<ProductResponse> results = new PagedResponse<ProductResponse>(items,new PageMetadata(totalCount, validatedPageSize, validatedPageNumber));
                
                    return Results.Ok(results);
                });
        } 
}