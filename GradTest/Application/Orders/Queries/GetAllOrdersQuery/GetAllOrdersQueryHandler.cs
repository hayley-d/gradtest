using GradTest.Persistence;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IList<GetAllOrdersQueryResponse>>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllOrdersQueryHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<IList<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();

        return await _dbContext.Orders
            .Select(order => new GetAllOrdersQueryResponse())
            .ToListAsync(cancellationToken);
    }
}