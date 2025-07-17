using GradTest.API.Routes;
using GradTest.Application.Orders.Commands.CreateOrderCommand;
using GradTest.Application.Orders.Queries.GetAllOrders;
using GradTest.Application.Orders.Queries.GetOrderById;
using GradTest.Application.Orders.Queries.GetOrderByUser;
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
        app.MapGet(ApiRoutes.Orders.GetAll, async (GetAllOrdersQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        app.MapGet(ApiRoutes.Orders.GetById, async (GetOrderByIdQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);;
        });
        
        app.MapGet(ApiRoutes.Orders.GetByUserId, async (GetOrderByUserQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}