namespace StoreAPI.Entities.Models;

public class User
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string FatherLN { get; set; }
    public string MotherLN { get; set; }
    public int DocumentType { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime LogDateCrate { get; set; }
    public DateTime LogDateModified { get; set; }
    public short LogState { get; set; }
}