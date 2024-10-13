namespace SmartBots.Application.Common;
public class Paging
{
    private const int MinPageSize = 1;
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;
    private const int MinPageNumber = 1;

    private int pageNumber = MinPageNumber;
    private int pageSize = DefaultPageSize;

    public int PageNumber
    {
        get => pageNumber;
        set => pageNumber = Math.Max(value, MinPageNumber);
    }

    public int PageSize
    {
        get => pageSize;
        set => pageSize = Math.Min(Math.Max(value, MinPageSize), MaxPageSize);
    }
}
