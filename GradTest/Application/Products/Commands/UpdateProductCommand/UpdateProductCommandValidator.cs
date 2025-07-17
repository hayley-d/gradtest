using FluentValidation;
using GradTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
   public UpdateProductCommandValidator(ApplicationDbContext context)
   {
      RuleFor(product => product.ProductId)
         .NotEmpty().WithMessage("Product ID must be provided.")
         .MustAsync(async (productId, cancellation) =>
            await context.Products.AnyAsync(p => p.Id == productId, cancellation))
         .WithMessage("Product with the given ID does not exist.");
   }
}