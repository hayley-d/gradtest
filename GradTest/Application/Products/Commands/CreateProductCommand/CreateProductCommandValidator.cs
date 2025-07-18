using FluentValidation;
using FluentValidation.Validators;
using GradTest.Domain.Enums;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must be between 3 and 100 characters.")
            .MinimumLength(3).WithMessage("Name must be between 3 and 100 characters.");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(350).WithMessage("Description must not exceed 350 characters.");

        RuleFor(command => command.CategoryName)
            .NotNull().WithMessage("CategoryName is required.")
            .NotEmpty().WithMessage("CategoryName must be provided.")
            .Must(BeAValidCategory).WithMessage("CategoryName is not valid.").WithMessage($"CategoryName must be one of: {string.Join(", ", Category.List.Select(c => c.Name))}");

        RuleFor(command => command.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive number.")
            .LessThan(decimal.MaxValue).WithMessage("Price value is too large."); 

        RuleFor(command => command.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be a positive number.")
            .LessThan(int.MaxValue).WithMessage("Stock quantity is too large."); 
    }
    
    private static bool BeAValidCategory(string categoryName)
    {
        return Category.TryFromName(categoryName, ignoreCase: true, out _);
    }
}