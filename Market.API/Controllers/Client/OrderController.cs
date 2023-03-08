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
            var order = await DatabaseContainer.Order.GetOneById(id);
            return Ok(order);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrdersList(int skip, int take)
        {
            var collection = await DatabaseContainer.Order.GetProductsRange(skip, take);
            return Ok(collection);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> FindOrdersByUserId(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
            return Ok(orders);
        }
    
    
        [HttpGet]
        public async Task<IActionResult> GetGroupedOrdersByDeliveryAddress(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        
            var groupedOrders = orders.GroupBy(o => new {o.UserId, o.DeliveryAddress})
                .Select(g => new 
                {
                    g.Key.DeliveryAddress,
                    OrderIds = string.Join(",", g.Select(o => o.Id)),
                    ProductIds = string.Join(",", g.Select(o => o.OrderProducts)),
                    CreatedAt = g.Max(o => o.CreatedAt),
                    g.Key.UserId,
                });
            
            return Ok(groupedOrders);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFullOrderInformation(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);

            var result = orders.Select(o => new
            {
               OrderID = o.Id,
               UserID = o.UserId,
               DeliveryAddress = o.DeliveryAddress,
               CreatedAt = o.CreatedAt,
               ProductsInOrder = o.OrderProducts.Select(op => new
                {
                    Product = new
                    {
                        op.ProductModel.Id,
                        op.ProductModel.Title,
                        op.ProductModel.Description,
                        op.ProductModel.Price
                    }
                }).ToList(),
               
            }).ToList();
            
            return Ok(result);
        
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderById(int userId, int orderId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var order = await DatabaseContainer.Order.GetOneById(orderId);

            if (user.Id != order.UserId)
            {
                return BadRequest($"User {user.Id} tried to delete order: {order.Id}. Cannot complete this operation because it is not a user's order.");
            }

            await DatabaseContainer.Order.Delete(order);
            return Ok();

        }
    }
}