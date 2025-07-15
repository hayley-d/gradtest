namespace GradTest.Endpoints;

public static class ApiEndpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.GetProductById();
        app.ListProducts();
        app.CreateProduct();
        app.UpdateProduct();
        app.DeleteProduct();
    }
}