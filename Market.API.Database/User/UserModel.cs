using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Market.API.Database.OrderProduct;

namespace Market.API.Database.User;

public class UserModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [JsonIgnore]
    public List<OrderProductModel> OrderProduct { get; set; }


    public static UserModel CreateModel(string firstName, string lastName)
    {
        return new UserModel()
        {
            FirstName = firstName,
            LastName = lastName,
            OrderProduct = new List<OrderProductModel>()
        };
    }
}