using GradTest.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Queries.GetAllOrdersQuery;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IList<GetAllOrdersQueryResponse>>
{
    private readonly ApplicationDbContext _dbContext;

    public GetAllOrdersQueryHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IList<GetAllOrdersQueryResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Orders
            .Select(order => new GetAllOrdersQueryResponse())
            .ToListAsync(cancellationToken);
    }
}