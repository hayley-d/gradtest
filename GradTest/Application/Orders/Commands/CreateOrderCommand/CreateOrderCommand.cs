using GradTest.Domain.Entities;
using MediatR;

namespace GradTest.Application.Orders.Commands.CreateOrderCommand;

public sealed record CreateOrderCommand(List<CreateOrderCommand.Product> Products) : IRequest<CreateOrderCommandResponse>
{
   public record Product(Guid ProductId, int Quantity)
   {
      public OrderProduct Convert() => new() { ProductId = ProductId, Quantity = Quantity };
   }
}