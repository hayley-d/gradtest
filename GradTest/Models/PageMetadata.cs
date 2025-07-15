namespace GradTest.Models;

public class PageMetadata
{
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

    public PageMetadata(int totalCount, int pageSize, int pageNumber)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        PageNumber = pageNumber;
    }
}
