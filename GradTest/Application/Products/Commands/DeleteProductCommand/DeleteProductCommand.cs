using MediatR;

namespace GradTest.Application.Products.Commands.DeleteProductCommand;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest;
