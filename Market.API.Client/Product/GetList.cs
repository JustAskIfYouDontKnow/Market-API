using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Market.API.Client.Product;

public class GetList
{
    [Required]
    public int Skip { get; set; }
    
    [Required]
    public int Take { get; set; }


    public class Response
    {
        public List<Payload.Product> Products { get; set; }
    }
}