using System.Data;
using FluentValidation;
using GradTest.Infrastructure.Persistence;
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

      RuleFor(product => product.Name)
         .NotEmpty().WithMessage("Product name must be provided.")
         .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
         .MaximumLength(100).WithMessage("Product name must be at most 100 characters long.");
      
      RuleFor(product => product.Description)
         .NotEmpty().WithMessage("Product description must be provided.")
         .MaximumLength(350).WithMessage("Product description must be at most 350 characters long.");

      RuleFor(product => product.CategoryValue)
         .NotNull().WithMessage("Category value must be provided.")
         .LessThanOrEqualTo(3)
         .WithMessage("Category value must be greater than or equal to 0 and less than or equal to 3.")
         .GreaterThanOrEqualTo(0)
         .WithMessage("Category value must be greater than or equal to 0 and less than or equal to 3.");

      RuleFor(product => product.Price)
         .NotNull().WithMessage("Price must be provided.")
         .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.")
         .LessThan(decimal.MaxValue).WithMessage("Price value is too large.");
         
      RuleFor(product => product.StockQuantity)
         .NotNull().WithMessage("Stock quantity must be provided.")
         .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be greater than or equal to 0.")
         .LessThan(int.MaxValue).WithMessage("Stock quantity value is too large.");
   }
}