using GradTest.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, ListProductsQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public ListProductsQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ListProductsQueryResponse> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var productsQuery = _dbContext.Products.AsQueryable();

        if (request.CategoryName is not null)
            productsQuery = productsQuery.Where(p => p.Category.Name.Equals(request.CategoryName)); 
        
        productsQuery = productsQuery.Where(p => p.Price >= request.MinPrice);
        productsQuery = productsQuery.Where(p => p.Price <= request.MaxPrice);
                
        var totalCount = await productsQuery.CountAsync(cancellationToken: cancellationToken);
                
        var items = await productsQuery
            .OrderBy(p => p.Name)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken: cancellationToken);

        var results = new ListProductsQueryResponse(items,new ListProductsQueryResponse.ListProductsPageMetadata(totalCount, request.PageSize, request.PageNumber));
        return results;
    }
}