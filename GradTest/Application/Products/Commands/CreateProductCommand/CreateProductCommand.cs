using GradTest.Domain.Enums;
using MediatR;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public sealed record CreateProductCommand (string Name, string Description, int CategoryValue, decimal Price, int StockQuantity) : IRequest<CreateProductCommandResponse>
{
    public Category Category => Category.FromValue(CategoryValue);
}