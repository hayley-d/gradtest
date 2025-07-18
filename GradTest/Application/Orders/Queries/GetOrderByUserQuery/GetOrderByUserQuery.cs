using MediatR;

namespace GradTest.Application.Orders.Queries.GetOrderByUserQuery;

public sealed record GetOrderByUserQuery : IRequest<IList<GetOrderByUserQueryResponse>>;