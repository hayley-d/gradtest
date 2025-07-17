using MediatR;

namespace GradTest.Application.Orders.Queries.GetOrderByUser;

public sealed record GetOrderByUserQuery : IRequest<IList<GetOrderByUserQueryResponse>>;