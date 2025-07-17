using System.ComponentModel.DataAnnotations;
using GradTest.Services;
using GradTest.Models;
using GradTest.Persistence;
using GradTest.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.CreateOrder;

public static class CreateOrder
{
    public static void MapCreateOrder(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/orders", async (HttpContext httpContext,ApplicationDbContext context, [FromBody] CreateOrderRequest req, IExchangeRateService exchangeRateService) =>
        {
            await AuthenticationMiddleware.UserAuthorize(httpContext);
                    
            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            } 
            
            var userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }
            
            var validationResults = new List<ValidationResult>();
            
            var validationContext = new ValidationContext(req);
            
            if (!Validator.TryValidateObject(req, validationContext, validationResults, validateAllProperties: true))
            {
                var errors = validationResults
                    .GroupBy(r => r.MemberNames.FirstOrDefault() ?? "General")
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(r => r.ErrorMessage ?? "Invalid value").ToArray());

                return Results.ValidationProblem(errors);
            }
            
            var latestRate = await context.ExchangeRates
                .OrderByDescending(r => r.Date)
                .Select(r => r.ZAR)
                .FirstOrDefaultAsync();

            if (latestRate == 0)
            {
                var liveRate = await exchangeRateService.GetExchangeRateAsync();
                
                if (liveRate is not null)
                {
                    context.ExchangeRates.Add(liveRate);
                    await context.SaveChangesAsync();
                    latestRate = liveRate.ZAR;
                }
                else
                {
                    return Results.BadRequest("Exchange rate not available.");
                } 
            }
            
            foreach (var product in req.Products)
            {
                try
                {
                    var dbProduct = await context.Products.FindAsync(product.ProductId);
                    
                    if (dbProduct is null)
                    {
                        throw new Exception($"Product {product.ProductId} was not found.");
                    }
                    else if (dbProduct.StockQuantity < product.Quantity)
                    {
                       throw new Exception($"Insufficient sock for product {product.ProductId}."); 
                    }
                    else
                    {
                        dbProduct.StockQuantity -= product.Quantity;
                        context.Products.Update(dbProduct);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                   return Results.Problem(ex.Message); 
                }
                
            }
            
            var products = req.Products
                .Select(product => product.Convert())
                .ToList();

            var newOrder = new Order {
                UserId = userId,
                Products = products,
                ZarToUsd = latestRate,
            };
            
            await context.Orders.AddAsync(newOrder);
            
            await context.SaveChangesAsync();
            
            return Results.Created($"/orders/{newOrder.Id}", new CreateOrderResponse(newOrder));
        });
        
    }
}