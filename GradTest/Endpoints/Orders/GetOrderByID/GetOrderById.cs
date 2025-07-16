using GradTest.Endpoints.Orders.GetOrdersByUser;
using GradTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.GetOrderByID;

public static class GetOrderById
{
    public static void MapGetOrderById(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/orders/{id}", async (HttpContext httpContext,ApplicationDbContext context,Guid id) =>
        {
            var userId = httpContext.User.FindFirst("sub")?.Value;
            
            if (string.IsNullOrEmpty(userId))
                return Results.Unauthorized();

            var order = await context.Orders
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
                return Results.NotFound();

            if (order.UserId != userId)
                return Results.Forbid();

            return Results.Ok(new GetOrderByIdResponse(order));
        });
    } 
}