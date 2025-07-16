using GradTest.Persistence;
using GradTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.GetOrdersByUser;

public static class GetOrdersByUser
{
    public static void MapGetOrdersByUser(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/orders", async (HttpContext httpContext,ApplicationDbContext context) =>
        {
            await AuthenticationMiddleware.UserAuthorize(httpContext);
                    
            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            } 
            
            string? userId = await AuthenticationMiddleware.GetUserID(httpContext);
            
            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }
            
            var orders = await context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .ToListAsync();

            var response = orders.Select(o => new GetOrdersByUserResponse(o)).ToList();

            return Results.Ok(response);
        });
    }
}