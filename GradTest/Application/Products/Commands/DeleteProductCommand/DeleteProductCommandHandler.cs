using GradTest.Persistence;
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
        var httpContext = _httpContextAccessor.HttpContext!;
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();
        
        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var product = await _dbContext.Products.FindAsync([request.ProductId], cancellationToken: cancellationToken);
            
        _dbContext.Products.Remove(product);
            
        await _dbContext.SaveChangesAsync(cancellationToken); 
    }
}