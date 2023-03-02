using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.API.Database.Order;

public interface IOrderRepository
{
    Task<OrderModel> Create(int userId, string shippingAddress);
}