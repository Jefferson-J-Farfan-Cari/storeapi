namespace StoreAPI.Entities.Dto;

public class CollectionResponse <T>
{
    public List<T> Data { get; set; }
    public int Page { get; set; }
    public int Count { get; set; }
}