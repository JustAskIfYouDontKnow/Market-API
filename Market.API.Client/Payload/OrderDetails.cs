using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Market.API.Client.Payload;

public class OrderDetails
{
    [Required]
    public int? OrderId { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public string DeliveryAddress { get; set; } 
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    [Required]
    public List<ProductDetails> ProductsInOrder { get; set; }
}