namespace StoreAPI.Entities.Dto;

public class ProductDto : GenericDto
{
    
    public CategoryDto Category { get; set; }
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
}