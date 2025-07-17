using MediatR;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public sealed record ListProductsQuery : IRequest<ListProductsQueryResponse>;