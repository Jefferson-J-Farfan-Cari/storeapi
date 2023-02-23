namespace StoreAPI.Entities.Dto;

public class CategoryDto : GenericDto
{
    public int IdCategory { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}