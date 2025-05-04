namespace RESTfullAPI.ViewModels.Pagination.Requests;

public class CursorPaginationFilterRequest
{
    public string? Cursor { get; set; }

    public int PageSize { get; set; } = 10;

    public string? SearchKeyword { get; set; }
}
