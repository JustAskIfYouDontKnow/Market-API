using System.ComponentModel.DataAnnotations;

namespace Market.API.Client.Payload;

public class ProductDetails
{
    [Required] 
    public int Id { get; set; }
    
    [Required] 
    public int? OrderId { get; set; }
    
    [Required] 
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
}