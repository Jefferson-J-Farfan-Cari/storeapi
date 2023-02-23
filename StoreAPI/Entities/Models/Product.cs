namespace StoreAPI.Entities.Models;

public class Product
{
    public int IdProduct { get; set; }
    public int IdCategory { get; set; }
    public int IdStore { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime LogDateCrate { get; set; }
    public DateTime LogDateModified { get; set; }
    public short LogState { get; set; }
    
    public virtual Category Category { get; set; }
    public virtual Store Store { get; set; }
}