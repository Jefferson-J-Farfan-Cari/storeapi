using System.Text.Json.Serialization;

namespace StoreAPI.Entities.Dto;

public class UserDto : GenericDto
{
    public int IdUser { get; set; }
    public int DocumentType { get; set; }
    [JsonIgnore (Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Password { get; set; }
    public string Name { get; set; }
    public string FatherLN { get; set; }
    public string MotherLN { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
}