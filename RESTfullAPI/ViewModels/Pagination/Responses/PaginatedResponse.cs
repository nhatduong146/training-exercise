namespace RESTfullAPI.ViewModels.Pagination.Responses;

public class PaginatedResponse<T>
{
    public int PageNumber { get; private set; }

    public int PageSize { get; private set; }

    public int TotalPages { get; private set; }

    public int TotalCount { get; private set; }

    public IEnumerable<T> Data { get; set; }

    public PaginatedResponse(int totalCount, int pageNumber, int pageSize, IEnumerable<T> data)
    {
        TotalCount = totalCount;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
    }
}
