namespace RESTfullAPI.ViewModels.Pagination.Requests;

public class PaginationFilterRequest
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SearchKeyword { get; set; }

    public string? SortColumn { get; set; }

    public SortType SortType { get; set; } = SortType.Ascending;
}

public enum SortType
{
    Ascending = 1, 
    Descending = -1
}