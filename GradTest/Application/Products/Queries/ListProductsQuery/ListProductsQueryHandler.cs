using GradTest.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, ListProductsQueryResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ListProductsQueryHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ListProductsQueryResponse> Handle(ListProductsQuery request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        await AuthenticationMiddleware.UserAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();

        return new ListProductsQueryResponse(null, null);
    }
}