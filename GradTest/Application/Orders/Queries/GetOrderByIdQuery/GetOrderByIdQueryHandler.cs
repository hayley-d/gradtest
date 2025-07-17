using GradTest.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetOrderById;

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
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();

        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var order = await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == query.OrderId, cancellationToken: cancellationToken);

        if (order is null)
            throw new KeyNotFoundException();

        if (order.UserId != userId)
            throw new UnauthorizedAccessException();

        return new GetOrderByIdQueryResponse(order);
    }
}