using GradTest.Endpoints.Orders.GetOrdersByUser;
using GradTest.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.GetAllOrders;

public static class GetAllOrders
{
    public static void MapGetAllOrders(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/orders", async (HttpContext httpContext,ApplicationDbContext context) =>
        {
            var orders = await context.Orders
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .ToListAsync();

            var response = orders.Select(o => new GetAllOrdersResponse(o)).ToList();
            return Results.Ok(response); 
        }).RequireAuthorization();
    }
}