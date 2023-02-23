using System.Text.Json.Serialization;

namespace StoreAPI.Entities.Dto;

public class GenericDto
{
    [JsonIgnore (Condition = JsonIgnoreCondition.Always)]
    public DateTime LogDateCrate { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.Always)]
    public DateTime LogDateModified { get; set; }
    
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public short LogState { get; set; }
}