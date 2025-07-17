using GradTest.Application.Orders.Queries.GetOrderById;
using GradTest.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetOrderByUser;

public class GetOrderByUserQueryHandler : IRequestHandler<GetOrderByUserQuery, IList<GetOrderByUserQueryResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetOrderByUserQueryHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IList<GetOrderByUserQueryResponse>> Handle(GetOrderByUserQuery query, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();

        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        return await _dbContext.Orders
            .Where(order => order.UserId == userId)
            .Select(order => new GetOrderByUserQueryResponse(order))
            .ToListAsync(cancellationToken: cancellationToken);
    }
}