using MediatR;

namespace GradTest.Application.Products.Queries.GetProductByIdQuery;

public sealed record GetProductByIdQuery(Guid ProductId) : IRequest<GetProductByIdQueryResponse>;