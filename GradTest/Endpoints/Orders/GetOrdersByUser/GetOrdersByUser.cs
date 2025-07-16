using GradTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.GetOrdersByUser;

public static class GetOrdersByUser
{
    public static void MapGetOrdersByUser(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/orders", async (HttpContext httpContext,ApplicationDbContext context) =>
        {
            string? userId = httpContext.User.FindFirst("sub")?.Value;
            
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