using GradTest.Domain.Enums;
using MediatR;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public sealed record CreateProductCommand (string Name, string Description, string CategoryName, decimal Price, int StockQuantity) : IRequest<CreateProductCommandResponse>;