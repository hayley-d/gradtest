using GradTest.Models;
using GradTest.Persistence;
using GradTest.Services;
using GradTest.Utils;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
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
    
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext!;
        await AuthenticationMiddleware.UserAuthorize(httpContext);

        if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            throw new UnauthorizedAccessException();
        
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
            Products = orderProducts,
            ZarToUsd = latestRate,
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResponse(order);
    }
}