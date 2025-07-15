namespace GradTest.Models;

public class PageMetadata
{
    public int TotalCount { private get; set; }
    public int PageSize { private get; set; }
    public int PageNumber { private get; set; }
    public int TotalPages { private get; set; }

    public PageMetadata(int totalCount, int pageSize, int pageNumber, int totalPages)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        PageNumber = pageNumber;
        TotalPages = totalPages;
    }
}