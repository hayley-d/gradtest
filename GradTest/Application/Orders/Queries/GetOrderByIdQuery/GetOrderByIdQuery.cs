using MediatR;

namespace GradTest.Application.Orders.Queries.GetOrderByIdQuery;

public sealed record GetOrderByIdQuery (Guid OrderId) : IRequest<GetOrderByIdQueryResponse>;