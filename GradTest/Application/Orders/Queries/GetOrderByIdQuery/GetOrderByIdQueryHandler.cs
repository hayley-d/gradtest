using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetOrderByIdQuery;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderByIdQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderByIdQueryHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<GetOrderByIdQueryResponse> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var order = await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == query.OrderId, cancellationToken: cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("No order found with the specified id.");

        return new GetOrderByIdQueryResponse(order);
    }
}