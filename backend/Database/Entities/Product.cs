namespace backend.Entities;

public class Product : IdEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public string PictureUrl { get; set; }
    
    public ProductType ProductType { get; set; }
    
    public int ProductTypeId { get; set; }
    
    public Brand Brand { get; set; }
    
    public int ProductBrandId { get; set; }
}