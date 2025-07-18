using GradTest.API.Routes;
using GradTest.Application.Products.Commands.CreateProductCommand;
using GradTest.Application.Products.Commands.DeleteProductCommand;
using GradTest.Application.Products.Commands.UpdateProductCommand;
using GradTest.Application.Products.Queries.GetProductByIdQuery;
using GradTest.Application.Products.Queries.ListProductsQuery;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GradTest.API.Endpoints.Products;

public static class MapProductsEndpoint
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        app
            .MapProductCommands()
            .MapProductQueries();
    }

    private static IEndpointRouteBuilder MapProductCommands(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Products.Create, async ([FromBody] CreateProductCommand command, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/products/{result.Id}", result);
        }).RequireAuthorization("Admin");
        
        app.MapPut(ApiRoutes.Products.Update, async ([FromBody] UpdateProductCommand command, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        }).RequireAuthorization("Admin");
        
        app.MapDelete(ApiRoutes.Products.Delete, async ([FromBody] DeleteProductCommand command, [FromServices] IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        }).RequireAuthorization("Admin");
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapProductQueries(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiRoutes.Products.List, async ([AsParameters] ListProductsQuery query, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        app.MapGet(ApiRoutes.Products.GetById, async ([AsParameters] GetProductByIdQuery query, [FromServices] IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        return app;
    }
    
    
}