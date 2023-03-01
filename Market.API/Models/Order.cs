using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.API.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        public string ShippingAddress { get; set; } 
        
        public List<Product> Products { get; set; }
    }
}