using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.API.Client.Product;

public class GetUserProducts
{
    [Required]
    public int UserId { get; set; }
    
    
    public class ResponseListByUserId
    {
        public List<Payload.Product> Products { get; set; }
    }
}