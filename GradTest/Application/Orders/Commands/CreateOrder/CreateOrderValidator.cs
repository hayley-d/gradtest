using FluentValidation;

namespace GradTest.Application.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.Products)
            .NotEmpty().WithMessage("At least one product must be included.")
            .Must(HaveNoDuplicates).WithMessage("Duplicate product IDs are not allowed.");

        RuleForEach(x => x.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");
        });
    }    
    
    private bool HaveNoDuplicates(List<CreateOrderCommand.Product> products)
    {
        return products
            .Select(p => p.ProductId)
            .Distinct()
            .Count() == products.Count;
    } 
}