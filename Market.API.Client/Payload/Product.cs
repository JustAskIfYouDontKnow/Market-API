using System.ComponentModel.DataAnnotations;

namespace Market.API.Client.Payload;

public class Product
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }


    [Required]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

}