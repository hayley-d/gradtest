using FluentValidation;

namespace GradTest.Application.Orders.Commands.CreateOrderCommand;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Products)
            .NotEmpty().WithMessage("At least one product must be included.")
            .Must(HaveNoDuplicates).WithMessage("Duplicate product IDs are not allowed.");

        RuleForEach(command => command.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");
        });
    }    
    
    private bool HaveNoDuplicates(List<Commands.CreateOrderCommand.CreateOrderCommand.Product> products)
    {
        return products
            .Select(product => product.ProductId)
            .Distinct()
            .Count() == products.Count;
    } 
}