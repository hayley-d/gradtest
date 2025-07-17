using GradTest.Models;
using MediatR;
namespace GradTest.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(List<CreateOrderCommand.Product> Products) : IRequest<CreateOrderResponse>
{
   public record Product(Guid ProductId, int Quantity)
   {
      public OrderProduct Convert() => new() { ProductId = ProductId, Quantity = Quantity };
   }
}