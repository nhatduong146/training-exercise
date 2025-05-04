namespace RESTfullAPI.ViewModels.Pagination.Responses;

public class CursorPaginatedResponse<T>
{
    public string? NextCursor { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public IEnumerable<T> Data { get; set; }

    public CursorPaginatedResponse(string? nextCursor, int totalCount, int pageSize, IEnumerable<T> data)
    {
        NextCursor = nextCursor;
        TotalCount = totalCount;
        PageSize = pageSize;
        Data = data;
    }
}
