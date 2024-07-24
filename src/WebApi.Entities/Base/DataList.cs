namespace WebApi.Entities.Base;

public record DataList<T>
{
    public DataList()
    {
        List = new List<T>();
    }

    public IEnumerable<T> List { get; init; }
    public int TotalRecord { get; init; }
}