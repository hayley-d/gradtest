using GradTest.API.Routes;
using GradTest.Application.Orders.Commands.CreateOrderCommand;
using MediatR;

namespace GradTest.API.Endpoints.Orders;

public static class MapOrderEndpoints
{
    public static void MapOrdersEndpoints(this IEndpointRouteBuilder app)
    {
        app
            .MapOrdersCommands()
            .MapOrdersQueries();
    }

    private static IEndpointRouteBuilder MapOrdersCommands(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Orders.Create, async (CreateOrderCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/orders/{result.Id}", result);
        });

        return app;
    }

    private static IEndpointRouteBuilder MapOrdersQueries(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Orders.GetAll, async (GetAllOrders query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        app.MapGet(ApiRoutes.Orders.GetById, async (GetOrderById query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return result is not null ? Results.Ok(result) : Results.NotFound();;
        });
        
        app.MapGet(ApiRoutes.Orders.GetByUserId, async (GetOrdersByUser query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}