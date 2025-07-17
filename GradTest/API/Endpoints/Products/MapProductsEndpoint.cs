using GradTest.API.Routes;
using GradTest.Application.Products.Commands.CreateProductCommand;
using GradTest.Application.Products.Commands.DeleteProductCommand;
using GradTest.Application.Products.Commands.UpdateProductCommand;
using GradTest.Application.Products.Queries.GetProductByIdQuery;
using GradTest.Application.Products.Queries.ListProductsQuery;
using GradTest.Endpoints.Products.CreateProduct;
using GradTest.Endpoints.Products.DeleteProduct;
using GradTest.Endpoints.Products.GetProductByID;
using GradTest.Endpoints.Products.ListProducts;
using GradTest.Endpoints.Products.UpdateProduct;
using MediatR;

namespace GradTest.API.Endpoints.Products;

public static class MapProductsEndpoint
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetProductById();
        app.MapListProducts();
    }

    private static IEndpointRouteBuilder MapProductCommands(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Products.Create, async ((CreateProductCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/products/{result.Id}", result);
        });
        
        app.MapPut(ApiRoutes.Products.Update, async ((UpdateProductCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/products/{result.Id}", result);
        });
        
        app.MapDelete(ApiRoutes.Products.Delete, async ((DeleteProductCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/products/{result.Id}", result);
        });
        
        return app;
    }
    
    private static IEndpointRouteBuilder MapProductQueries(this IEndpointRouteBuilder app)
    {
        app.MapPost(ApiRoutes.Products.List, async ((ListProductsQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        app.MapPost(ApiRoutes.Products.GetById, async ((GetProductByIdQuery query, IMediator mediator) =>
        {
            var result = await mediator.Send(query);
            return Results.Ok(result);
        });
        
        return app;
    }
    
    
}