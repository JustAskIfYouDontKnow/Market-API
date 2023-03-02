using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.Order
{
    public class OrderModel : AbstractModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public UserModel UserModel { get; set; }
        
        [Required]
        public string ShippingAddress { get; set; } 
        
        [Required]
        public DateTime CreatedAt { get; set; }
        
        public List<ProductModel> Products { get; set; }


        public static OrderModel CreateModel(int userId, string shippingAddress)
        {
            return new OrderModel()
            {
                UserId = userId,
                ShippingAddress = shippingAddress,
                CreatedAt = DateTime.Now,
                Products = new List<ProductModel>()
            };
        }
    }
}