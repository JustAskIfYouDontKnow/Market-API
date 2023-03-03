using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client
{

    public class OrderController : AbstractClientController
    {

        public OrderController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(int userId, IEnumerable<int> productIds, string deliveryAddress)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var createdOrder = await DatabaseContainer.OrderService.Create(user, productIds, deliveryAddress);
            return Ok(createdOrder);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await DatabaseContainer.OrderProduct.GetOneById(id);
            return Ok(order);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrdersList(int skip, int take)
        {
            var collection = await DatabaseContainer.OrderProduct.GetProductsRange(skip, take);
            return Ok(collection);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> FindOrdersByUserId(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var totalOrder = await DatabaseContainer.OrderProduct.FindOrdersByUserId(user);
            return Ok(totalOrder);
        }
    
    
        [HttpGet]
        public async Task<IActionResult> GetGroupedOrdersByDeliveryAddress(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var orders = await DatabaseContainer.OrderProduct.FindOrdersByUserId(user);
            var groupedOrders = orders.GroupBy(o => new { o.UserId, o.DeliveryAddress })
                .Select(g => new {
                    OrderIds = string.Join(",", g.Select(o => o.Id)),
                    g.Key.UserId,
                    ProductIds = string.Join(",", g.Select(o => o.ProductId)),
                    g.Key.DeliveryAddress,
                    CreatedAt = g.Max(o => o.CreatedAt)
                });
            return Ok(groupedOrders);
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderById(int userId, int orderId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var order = await DatabaseContainer.OrderProduct.GetOneById(orderId);

            if (user.Id != order.UserId)
            {
                return BadRequest($"User {user.Id} tried to delete order: {order.Id}. Cannot complete this operation because it is not a user's order.");
            }

            await DatabaseContainer.OrderProduct.Delete(order);
            return Ok();

        }
    }
}