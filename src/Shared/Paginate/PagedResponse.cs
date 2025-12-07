using Z.PagedList;

namespace Autoparts.Api.Shared.Paginate;

public sealed class PagedResponse<T>
{
    public List<T> Items { get; private set; }
    public int TotalItemCount { get; private set; }
    public int PageSize { get; private set; }
    public int PageCount { get; private set; }
    public bool IsFirstPage { get; private set; }
    public bool IsLastPage { get; private set; }
    public bool HasPreviousPage { get; private set; }
    public bool HasNextPage { get; private set; }
    public int FirstItemOnPage { get; private set; }
    public int LastItemOnPage { get; private set; }
    public int PageNumber { get; private set; }

    public PagedResponse(IPagedList<T> pagedList)
    {
        Items = [.. pagedList];
        TotalItemCount = pagedList.TotalItemCount;
        PageSize = pagedList.PageSize;
        PageCount = pagedList.PageCount;
        IsFirstPage = pagedList.IsFirstPage;
        IsLastPage = pagedList.IsLastPage;
        HasPreviousPage = pagedList.HasPreviousPage;
        HasNextPage = pagedList.HasNextPage;
        FirstItemOnPage = pagedList.FirstItemOnPage;
        LastItemOnPage = pagedList.LastItemOnPage;
        PageNumber = pagedList.PageNumber;
    }

}
