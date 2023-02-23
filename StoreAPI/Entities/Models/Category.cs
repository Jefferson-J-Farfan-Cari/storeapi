namespace StoreAPI.Entities.Models;

public class Category
{
    public Category()
    {
        Product = new HashSet<Product>();
    }

    public int IdCategory { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime LogDateCrate { get; set; }
    public DateTime LogDateModified { get; set; }
    public short LogState { get; set; }
    
    public virtual ICollection<Product> Product { get; set; }
}