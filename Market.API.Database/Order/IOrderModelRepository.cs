using System.Collections.Generic;
using System.Threading.Tasks;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.Order;

public interface IOrderModelRepository
{
    Task<OrderModel> Create(UserModel user,  List<OrderProductModel> products, string deliveryAddress);

    Task<List<OrderModel>> FindOrdersByUserId(UserModel user);

    Task<OrderModel> GetOneById(int id);
    
    Task<List<OrderModel>> GetProductsRange(int skip, int take);

    Task<bool> Delete(OrderModel product);
}