using GradTest.Domain.Enums;
using MediatR;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public sealed record ListProductsQuery(
    string? CategoryName = null,
    decimal MaxPrice =  decimal.MaxValue,
    decimal MinPrice = 0,
    int PageNumber = 1,
    int PageSize = 10) : IRequest<ListProductsQueryResponse>;