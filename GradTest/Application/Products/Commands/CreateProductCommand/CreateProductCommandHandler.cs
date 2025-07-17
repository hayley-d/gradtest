using GradTest.Domain.Entities;
using GradTest.Persistence;
using GradTest.Utils;
using MediatR;

namespace GradTest.Application.Products.Commands.CreateProductCommand;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateProductCommandHandler(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        await AuthenticationMiddleware.AdminAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();
        
        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException(); 
        
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            CategoryValue = request.Category.Value,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity 
        };
                
        await _dbContext.Products.AddAsync(newProduct, cancellationToken);
                
        await _dbContext.SaveChangesAsync(cancellationToken);
                
        return new CreateProductCommandResponse(newProduct);
    }
}