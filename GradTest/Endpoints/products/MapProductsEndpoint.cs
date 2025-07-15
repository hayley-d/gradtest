namespace GradTest.Endpoints.products;

public static class MapProductsEndpoint
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetProductById();
        app.MapCreateProduct();
        app.MapListProducts();
        app.MapUpdateProduct();
        app.MapDeleteProduct();
    }
}