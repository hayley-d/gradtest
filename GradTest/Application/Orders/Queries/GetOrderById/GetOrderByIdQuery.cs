using GradTest.Endpoints.Orders.GetOrderByID;
using MediatR;

namespace GradTest.Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery (Guid OrderId) : IRequest<GetOrderByIdQueryResponse>;