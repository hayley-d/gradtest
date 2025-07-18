using FluentValidation;
using GradTest.Domain.Enums;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery> 
{
    public ListProductsQueryValidator()
    {
        RuleFor(command => command.CategoryName)
            .Must(BeAValidCategory).WithMessage("CategoryName is not valid.")
            .When(command => command.CategoryName is not null);
        
        RuleFor(query => query.MinPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum price must be a positive number.")
            .LessThan(query => query.MaxPrice).WithMessage("Minimum price must be less that the maximum price.");
        
       RuleFor(query => query.MaxPrice)
           .GreaterThan(query => query.MinPrice).WithMessage("Max price must be greater than Min price")
           .LessThan(decimal.MaxValue).WithMessage("Max price value is too large.");
       
       RuleFor(query => query.PageNumber)
           .GreaterThanOrEqualTo(0).WithMessage("Page number must be a positive number.")
           .LessThanOrEqualTo(int.MaxValue).WithMessage("Page number is too large.");
       
       RuleFor(query => query.PageSize)
           .GreaterThan(0).WithMessage("Page size must be greater than 0.")
           .LessThanOrEqualTo(int.MaxValue).WithMessage("Page size is too large.");
       
    }
    
    private static bool BeAValidCategory(string categoryName)
    {
        var res = Category.TryFromName(categoryName, ignoreCase: true, out _);
        
        return res;
    }
}