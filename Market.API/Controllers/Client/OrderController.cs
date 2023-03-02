using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client
{

    public class OrderController : AbstractClientController
    {
        
        public OrderController(IDatabaseContainer databaseContainer) : base(databaseContainer)
        { }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(int userId, IEnumerable<int> productIds, string deliveryAddress)
        {
            var user = await _databaseContainer.User.GetOneById(userId);
            var createdOrder = await _databaseContainer.OrderService.Create(user, productIds, deliveryAddress);
            return Ok(createdOrder);
        }
        
        [HttpGet]
        public async Task<IActionResult> FindOrder(int id)
        {
            var order = await _databaseContainer.OrderProduct.FindOneById(id);
            return Ok(order);
        }

        
        [HttpGet]
        public async Task<IActionResult> GetPaginatedOrders(int skip, int take)
        {
            var paginationOrder = await _databaseContainer.OrderProduct.GetProductsRange(skip, take);
            return Ok(paginationOrder);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> FindOrdersByUserId(int userId)
        {
            var user = await _databaseContainer.User.GetOneById(userId);
            var orders = await _databaseContainer.OrderProduct.FindListByUserId(userId);
            return Ok(orders);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderById(int userId, int orderId)
        {
            var user = await _databaseContainer.User.GetOneById(userId);
            var order = await _databaseContainer.OrderProduct.FindOneById(orderId);

            if (user.Id != order.UserId)
            {
                throw new Exception("Can't delete order");
            }

            await _databaseContainer.OrderProduct.Delete(order);
            return Ok();

        }
    }
}