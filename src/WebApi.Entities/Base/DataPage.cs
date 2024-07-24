namespace WebApi.Entities.Base;

public record DataPage<T> : DataList<T>
{
    public DataPage()
    {
        List = new List<T>();
    }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}