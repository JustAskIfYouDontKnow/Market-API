using System;
using System.Threading.Tasks;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database.OrderProduct;

public class OrderProductModelRepo : AbstractRepository<OrderProductModel>, IOrderProductModelRepo
{
    public OrderProductModelRepo(PostgresContext context) : base(context)
    {
    }


    public async Task<OrderProductModel> Create(UserModel user, ProductModel product, string deliveryAddress)
    {
        var model = OrderProductModel.CreateModel(user, product, deliveryAddress);

        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("Order product is not created");
        }

        return result;
    }
}