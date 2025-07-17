using GradTest.Domain.Entities;

namespace GradTest.Application.Products.Queries.ListProductsQuery;

public class ListProductsQueryResponse
{
    public IEnumerable<Product> Products { get; init; }
    public ListProductsPageMetadata Metadata { get; init; }

    public ListProductsQueryResponse(IEnumerable<Product> products, ListProductsPageMetadata metadata)
    {
        Products = products;
        Metadata = metadata;
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
}

