using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Market.API.Database.Order;
using Market.API.Database.Product;

namespace Market.API.Database.OrderProduct;
public class OrderProductModel : AbstractModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OrderId { get; set; }

    [ForeignKey("OrderId")]
    [JsonIgnore]
    public OrderModel OrderModel { get; set; }

    public int ProductId { get; set; }

    [ForeignKey("ProductId")]
    [JsonIgnore]
    public ProductModel ProductModel { get; set; }
}