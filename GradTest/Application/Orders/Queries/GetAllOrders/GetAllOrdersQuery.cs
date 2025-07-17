using GradTest.Models;
using MediatR;

namespace GradTest.Application.Orders.Queries.GetAllOrders;

public sealed record GetAllOrdersQuery : IRequest<IList<GetAllOrdersQueryResponse>>;