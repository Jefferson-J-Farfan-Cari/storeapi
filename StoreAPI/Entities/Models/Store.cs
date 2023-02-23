namespace StoreAPI.Entities.Models;

public class Store
{
    public Store()
    {
        Product = new HashSet<Product>();
    }
    
    public int IdStore { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public DateTime LogDateCrate { get; set; }
    public DateTime LogDateModified { get; set; }
    public short LogState { get; set; }
    
    public virtual ICollection<Product> Product { get; set; }
}