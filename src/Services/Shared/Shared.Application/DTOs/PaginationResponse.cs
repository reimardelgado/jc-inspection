namespace Shared.Application.DTOs;

public class PaginationResponse
{
    public int Page { get; }
    public int PageSize { get; }
    public int Total { get; }

    public PaginationResponse()
    {
        Page = 0;
        PageSize = 0;
        Total = 0;
    }

    public PaginationResponse(int page, int pageSize, int total)
    {
        Page = page;
        PageSize = pageSize;
        Total = total;
    }
}