using Vicgital.Application.Shared.Constants;

namespace Vicgital.Application.Shared.Pagination;

/// <summary>A page/size request, normalized so downstream code never has to guard against invalid values.</summary>
public sealed class PagedRequest
{
    public int PageNumber { get; }

    public int PageSize { get; }

    public PagedRequest(int pageNumber = PaginationDefaults.DefaultPageNumber, int pageSize = PaginationDefaults.DefaultPageSize)
    {
        PageNumber = pageNumber < 1 ? PaginationDefaults.DefaultPageNumber : pageNumber;
        PageSize = pageSize < 1
            ? PaginationDefaults.DefaultPageSize
            : Math.Min(pageSize, PaginationDefaults.MaxPageSize);
    }

    /// <summary>Number of items to skip to reach this page, for use with <c>Skip(...).Take(...)</c> queries.</summary>
    public int Skip => (PageNumber - 1) * PageSize;
}
