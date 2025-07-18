using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommand>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeleteProductCommandHandle(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync([request.ProductId], cancellationToken: cancellationToken);

        if (product == null)
            throw new KeyNotFoundException("No product found with matching id.");
            
        _dbContext.Products.Remove(product);
            
        await _dbContext.SaveChangesAsync(cancellationToken); 
    }
}