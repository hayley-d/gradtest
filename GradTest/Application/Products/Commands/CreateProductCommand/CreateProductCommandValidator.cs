using FluentValidation;
using FluentValidation.Validators;
using GradTest.Domain.Enums;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters")
            .MinimumLength(3).WithMessage("Name must be between 3 and 100 characters");

        RuleFor(command => command.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(350).WithMessage("Description must not exceed 350 characters");

        RuleFor(command => command.CategoryValue)
            .InclusiveBetween(0, 3).WithMessage("Category value must be between 0 and 3")
            .Must(value => Category.FromValue(value) is not null).WithMessage("Invalid category value.");

        RuleFor(command => command.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be a positive number.");

        RuleFor(command => command.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("StockQuantity must be a positive number.");
    }
}