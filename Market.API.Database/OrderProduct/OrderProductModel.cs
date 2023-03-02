using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.OrderProduct
{
    public class OrderProductModel : AbstractModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserModel UserModel { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public ProductModel ProductModel { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }


        public static OrderProductModel CreateModel(UserModel user, ProductModel product, string deliveryAddress)
        {
            return new OrderProductModel()
            {
                UserModel = user,
                ProductModel = product,
                DeliveryAddress = deliveryAddress,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}