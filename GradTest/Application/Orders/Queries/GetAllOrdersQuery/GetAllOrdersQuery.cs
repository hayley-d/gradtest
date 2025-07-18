using GradTest.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetAllOrdersQuery;

public sealed record GetAllOrdersQuery : IRequest<IList<GetAllOrdersQueryResponse>>;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IList<GetAllOrdersQueryResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllOrdersQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IList<GetAllOrdersQueryResponse>> Handle(global::GradTest.Application.Orders.Queries.GetAllOrdersQuery.GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Select(order => new GetAllOrdersQueryResponse())
            .ToListAsync(cancellationToken);
    }
}