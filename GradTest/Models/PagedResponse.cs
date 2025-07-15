namespace GradTest.Models;

public class PagedResponse<T>
{
    public IEnumerable<T> Products { get; init; }
    public PageMetadata Metadata { get; init; }

    public PagedResponse(IEnumerable<T> products, PageMetadata metadata)
    {
        Products = products ?? throw new ArgumentNullException(nameof(products));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
    }
}
