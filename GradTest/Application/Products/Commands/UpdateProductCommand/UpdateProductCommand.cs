using GradTest.Domain.Enums;
using MediatR;

namespace GradTest.Application.Products.Commands.UpdateProductCommand;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string Name,
    string Description,
    int CategoryValue,
    decimal Price,
    int StockQuantity) : IRequest
{
    public Category Category => Category.FromValue(CategoryValue); 
}