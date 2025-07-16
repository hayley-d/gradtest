using GradTest.Endpoints.Orders.GetOrdersByUser;
using GradTest.Persistence;
using GradTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace GradTest.Endpoints.Orders.GetAllOrders;

public static class GetAllOrders
{
    public static void MapGetAllOrders(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/admin/orders", async (HttpContext httpContext,ApplicationDbContext context) =>
        {
            await AuthenticationMiddleware.AdminAuthorize(httpContext);
                    
            if (httpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                return Results.Unauthorized();
            } 
            
            var response = await context.Orders.Select(o => new GetAllOrdersResponse(o)).ToListAsync();
            
            return Results.Ok(response); 
        }).WithDescription("Gets all orders on the system.");
    }
}