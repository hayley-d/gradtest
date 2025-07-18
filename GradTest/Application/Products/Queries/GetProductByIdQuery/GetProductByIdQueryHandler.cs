using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public GetProductByIdQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == query.ProductId, cancellationToken: cancellationToken);

        if (product is null)
            throw new KeyNotFoundException("Product not found.");

        return new GetProductByIdQueryResponse(product);
    }
}