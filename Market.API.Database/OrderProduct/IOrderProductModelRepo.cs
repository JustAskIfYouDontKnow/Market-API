using System.Collections.Generic;
using System.Threading.Tasks;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.OrderProduct;

public interface IOrderProductModelRepo
{
    Task<OrderProductModel> Create(UserModel user, ProductModel product, string deliveryAddress);
    Task<List<OrderProductModel>> FindOrdersByUserId(UserModel user);
}