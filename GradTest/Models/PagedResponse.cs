namespace GradTest.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Products { private get; set; }
    public PageMetadata Metadata { private get; set; }

    public PagedResponse(IEnumerable<T> products, int totalCount, int pageSize, int pageNumber, int totalPages)
    {
        Products = products;
        Metadata = new PageMetadata(totalCount,pageSize,pageNumber, totalPages);
    }
}