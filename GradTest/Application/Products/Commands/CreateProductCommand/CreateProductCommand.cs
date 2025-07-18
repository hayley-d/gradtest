using FluentValidation;
using GradTest.Domain.Entities;
using GradTest.Domain.Enums;
using GradTest.Infrastructure.Persistence;
using MediatR;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public sealed record CreateProductCommand (string Name, string Description, string CategoryName, decimal Price, int StockQuantity) : IRequest<CreateProductCommandResponse>;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly ApplicationDbContext _dbContext;

    public CreateProductCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = Category.FromName(request.CategoryName); 
        
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = category,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity 
        };
                
        await _dbContext.Products.AddAsync(newProduct, cancellationToken);
                
        await _dbContext.SaveChangesAsync(cancellationToken);
                
        return new CreateProductCommandResponse(newProduct);
    }
}

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
            .Must(BeAValidCategory).WithMessage("CategoryName is not valid.");

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
