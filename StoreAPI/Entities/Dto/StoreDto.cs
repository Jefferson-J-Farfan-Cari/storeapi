using System.Text.Json.Serialization;

namespace StoreAPI.Entities.Dto;

public class StoreDto : GenericDto
{
    public int IdStore { get; set; }
    public string Address { get; set; }
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Distance { get; set; }
    public string Name { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<ProductDto> Products { get; set; }
}