using FluentValidation;

namespace GradTest.Application.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Products)
            .NotNull().WithMessage("Product list cannot be null")
            .NotEmpty().WithMessage("At least one product must be included.")
            .Must(HaveNoDuplicates).WithMessage("Duplicate product IDs are not allowed.");

        RuleForEach(command => command.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.")
                .LessThan(int.MaxValue).WithMessage("Quantity value is too large.");
        });
    }    
    
    private bool HaveNoDuplicates(List<CreateOrderCommand.Product> products)
    {
        return products
            .Select(product => product.ProductId)
            .Distinct()
            .Count() == products.Count;
    } 
}