using System;
using System.Threading.Tasks;

namespace Market.API.Database.Order;

public class OrderRepository : AbstractRepository<OrderModel>, IOrderRepository
{
    public OrderRepository(PostgresContext context) : base(context)
    {
    }

    public async Task<OrderModel> Create(int userId, string shippingAddress)
    {
        var model = OrderModel.CreateModel(userId, shippingAddress);
        
        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("Order model is not created.");
        }

        return result;
    }
}