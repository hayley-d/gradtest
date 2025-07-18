using GradTest.API.Routes;
using GradTest.Application.Orders.Commands.CreateOrderCommand;
using GradTest.Application.Orders.Queries.GetAllOrdersQuery;
using GradTest.Application.Orders.Queries.GetOrderByIdQuery;
using GradTest.Application.Orders.Queries.GetOrderByUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        app.MapPost(ApiRoutes.Orders.Create, async ( [FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/orders/{result.Id}", result);
        }).RequireAuthorization("Admin");

        return app;
    }

    private static IEndpointRouteBuilder MapOrdersQueries(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Orders.GetAll, async ([AsParameters] GetAllOrdersQuery query, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        }).RequireAuthorization("Admin");

        app.MapGet(ApiRoutes.Orders.GetById, async ([AsParameters] GetOrderByIdQuery query, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
            ;
        });

        app.MapGet(ApiRoutes.Orders.GetByUserId, async ([AsParameters] GetOrderByUserQuery query, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });

        return app;
    }
}