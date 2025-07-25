using GradTest.API.Routes;
using GradTest.Application.Products.Commands.CreateProductCommand;
using GradTest.Application.Products.Commands.DeleteProductCommand;
using GradTest.Application.Products.Commands.UpdateProductCommand;
using GradTest.Application.Products.Queries.GetProductByIdQuery;
using GradTest.Application.Products.Queries.ListProductsQuery;
using MediatR;

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
        app.MapPost(ApiRoutes.Products.Create, async (CreateProductCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/products/{result.Id}", result);
        });
        
        app.MapPut(ApiRoutes.Products.Update, async (UpdateProductCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });
        
        app.MapDelete(ApiRoutes.Products.Delete, async (DeleteProductCommand command, IMediator mediator) =>
        {
            await mediator.Send(command);
            return Results.Ok();
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapProductQueries(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Products.List, async (ListProductsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        app.MapPost(ApiRoutes.Products.GetById, async (GetProductByIdQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        return app;
    }
    
    
}