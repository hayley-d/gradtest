using System.ComponentModel.DataAnnotations;
using GradTest.Domain.Entities;
using GradTest.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace GradTest.Endpoints.Products.ListProducts;

public class ListProductsResponse<T>
{
    public IEnumerable<T> Products { get; init; }
    public ListProductsPageMetadata Metadata { get; init; }

    public ListProductsResponse(IEnumerable<T> products, ListProductsPageMetadata metadata)
    {
        Products = products ?? throw new ArgumentNullException(nameof(products));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }
}

public class ListProductsProductResponse
{
    [SwaggerSchema("The unique identifier of the product.")]
    public Guid Id { get; init; }
    [SwaggerSchema("The name of the product.")]
    public string Name { get; init; }
    [SwaggerSchema("The description of the product.")]
    public string Description { get; init; }
    [SwaggerSchema("The category of the product.")]
    public int Category { get; init; }
    [SwaggerSchema("The price of the product in Rands.")]
    public decimal Price { get; init; }
    [SwaggerSchema("The current stock quantity of the product.")]
    public int StockQuantity { get; init; }

    public ListProductsProductResponse(Guid id, string name, string description, Category category, decimal price,
        int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category.Value;
        Price = price;
        StockQuantity = stockQuantity;
    }

    public ListProductsProductResponse(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Description = product.Description;
        Category = product.Category.Value;
        Price = product.Price;
        StockQuantity = product.StockQuantity;
    }
}

public class ListProductsPageMetadata
{
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public ListProductsPageMetadata(int totalCount, int pageSize, int pageNumber)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}