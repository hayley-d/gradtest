using GradTest.Endpoints.Products.CreateProduct;
using GradTest.Endpoints.Products.DeleteProduct;
using GradTest.Endpoints.Products.ListProducts;
using GradTest.Endpoints.Products.UpdateProduct;
using GradTest.Endpoints.Products.GetProductByID;

namespace GradTest.Endpoints.Products;

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