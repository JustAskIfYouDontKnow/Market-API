using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.API.Database.Order;

namespace Market.API.Database.Product
{
    public class ProductModel : AbstractModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public OrderModel OrderModel { get; set; }
        
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }


        public static ProductModel CreateModel(int orderId, string title, string description, decimal price)
        {
            return new ProductModel
            {
                OrderId = orderId,
                Title = title,
                Description = description,
                Price = price
            };
        }
    }
}