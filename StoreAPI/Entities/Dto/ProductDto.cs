using System.Text.Json.Serialization;

namespace StoreAPI.Entities.Dto;

public class ProductDto : GenericDto
{
    
    public int IdProduct { get; set; }
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int IdCategory { get; set; }
    public int IdStore { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public DateTime DueDate { get; set; }
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public CategoryDto Category { get; set; }
}