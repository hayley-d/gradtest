using GradTest.Domain.Entities;
using MediatR;
using FluentValidation;
using GradTest.Infrastructure.Persistence;
using GradTest.Services;
using GradTest.Utils;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Commands.CreateOrderCommand;

public sealed record CreateOrderCommand(List<CreateOrderCommand.Product> Products) : IRequest<CreateOrderCommandResponse>
{
   public record Product(Guid ProductId, int Quantity)
   {
      public OrderProduct Convert() => new() { ProductId = ProductId, Quantity = Quantity };
   }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateOrderCommandHandler(ApplicationDbContext dbContext, IExchangeRateService exchangeRateService,
        IHttpContextAccessor httpContextAccessor)
    {
       _dbContext = dbContext;
       _exchangeRateService = exchangeRateService;
       _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext!;

        var userId = await AuthenticationMiddleware.GetUserID(httpContext);
        
        if (string.IsNullOrEmpty(userId))
            throw new UnauthorizedAccessException();
        
        var latestRate = await _dbContext.ExchangeRates
            .OrderByDescending(rate => rate.Date)
            .Select(rate => rate.ZAR)
            .FirstOrDefaultAsync(cancellationToken);

        if (latestRate == 0)
        {
            var liveRate = await _exchangeRateService.GetExchangeRateAsync();
            
            if (liveRate is null)
                throw new NullReferenceException("Exchange rate not available.");        
            
            _dbContext.ExchangeRates.Add(liveRate);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            latestRate = liveRate.ZAR;
        }

        foreach (var product in request.Products)
        {
            var dbProduct = await _dbContext.Products.FindAsync([product.ProductId], cancellationToken);
            
            if (dbProduct is null)
                throw new InvalidOperationException($"Product {product.ProductId} not found.");

            if (dbProduct.StockQuantity < product.Quantity)
                throw new InvalidOperationException($"Insufficient stock for product {product.ProductId}.");        
            
            dbProduct.StockQuantity -= product.Quantity;
            
            _dbContext.Products.Update(dbProduct);
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        var orderProducts = request.Products
            .Select(product => product.Convert())
            .ToList();

        var order = new Order
        {
            UserId = userId,
            ZarToUsd = latestRate,
        };

        foreach (var orderProduct in orderProducts)
        {
            _dbContext.OrderProducts.Add(orderProduct);
        }

        _dbContext.Orders.Add(order);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderCommandResponse(order, orderProducts);
    }
}

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(command => command.Products)
            .NotNull().WithMessage("Product list cannot be null")
            .NotEmpty().WithMessage("At least one product must be included.")
            .Must(HaveNoDuplicates).WithMessage("Duplicate product IDs are not allowed.");

        RuleForEach(command => command.Products).ChildRules(product =>
        {
            product.RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.")
                .LessThan(int.MaxValue).WithMessage("Quantity value is too large.");
        });
    }    
    
    private bool HaveNoDuplicates(List<CreateOrderCommand.Product> products)
    {
        return products
            .Select(product => product.ProductId)
            .Distinct()
            .Count() == products.Count;
    } 
}