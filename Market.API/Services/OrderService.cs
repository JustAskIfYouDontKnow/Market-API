using Market.API.Models;

namespace Market.API.Services
{
    public class OrderService : IOrderService
    {

        public Task<List<Order>> GetOrders()
        {
            throw new NotImplementedException();
        }


        public Task<Order> GetOrder(int id)
        {
            throw new NotImplementedException();
        }


        public Task<Order> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }


        public Task<Order> UpdateOrder(int id, Order order)
        {
            throw new NotImplementedException();
        }


        public Task DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }
    }
}