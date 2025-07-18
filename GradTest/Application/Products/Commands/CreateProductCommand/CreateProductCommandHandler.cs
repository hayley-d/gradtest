using GradTest.Domain.Entities;
using GradTest.Domain.Enums;
using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

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