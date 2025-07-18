using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly ApplicationDbContext _dbContext;

    public UpdateProductCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync([request.ProductId], cancellationToken: cancellationToken);
        
        if (product is null)
            throw new KeyNotFoundException("Product not found.");
            
        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;

        _dbContext.Products.Update(product);
        
        await _dbContext.SaveChangesAsync(cancellationToken); 
    }
}