using GradTest.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateProductCommandHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();
        
        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var product = await _dbContext.Products.FindAsync([request.ProductId], cancellationToken: cancellationToken);
            
        product.Name = request.Name;
        product.Description = request.Description;
        product.Category = request.Category;
        product.Price = request.Price;
        product.StockQuantity = request.StockQuantity;

        _dbContext.Products.Update(product);
        
        await _dbContext.SaveChangesAsync(cancellationToken); 
    }
}