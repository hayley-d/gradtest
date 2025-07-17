using GradTest.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProductByIdQueryHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        await AuthenticationMiddleware.UserAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();

        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var product = await _dbContext.Products.FirstOrDefaultAsync(product => product.Id == query.ProductId, cancellationToken: cancellationToken);

        if (product is null)
            throw new KeyNotFoundException();

        return new GetProductByIdQueryResponse(product);
    }
}