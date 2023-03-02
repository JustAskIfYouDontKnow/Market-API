using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Market.API.Database.OrderProduct;

namespace Market.API.Database.Product
{
    public class ProductModel : AbstractModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [JsonIgnore]
        public List<OrderProductModel> OrderProducts { get; set; }


        public static ProductModel CreateModel(string title, string description, decimal price)
        {
            return new ProductModel
            {
                Title = title,
                Description = description,
                Price = price
            };
        }
    }
}