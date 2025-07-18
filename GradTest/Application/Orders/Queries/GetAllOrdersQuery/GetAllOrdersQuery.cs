using MediatR;

namespace GradTest.Application.Orders.Queries.GetAllOrdersQuery;

public sealed record GetAllOrdersQuery : IRequest<IList<GetAllOrdersQueryResponse>>;