using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.Order
{ 
    public class OrderModel : AbstractModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public UserModel UserModel { get; set; }

        public List<OrderProductModel> OrderProducts { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public static OrderModel CreateModel(UserModel user, List<OrderProductModel> orderProducts, string deliveryAddress)
        {
            return new OrderModel()
            {
                UserModel = user, 
                OrderProducts = orderProducts,
                DeliveryAddress = deliveryAddress,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}