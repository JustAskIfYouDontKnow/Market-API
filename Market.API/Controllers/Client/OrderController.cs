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
            var totalOrder = await DatabaseContainer.Order.FindOrdersByUserId(user);
            return Ok(totalOrder);
        }
    
    
        // [HttpGet]
        // public async Task<IActionResult> GetGroupedOrdersByDeliveryAddress(int userId)
        // {
        //     var user = await DatabaseContainer.User.GetOneById(userId);
        //     var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        //
        //     var groupedOrders = orders.GroupBy(o => new {o.UserId, o.DeliveryAddress})
        //         .Select(g => new 
        //         {
        //             g.Key.DeliveryAddress,
        //             OrderIds = string.Join(",", g.Select(o => o.Id)),
        //             ProductIds = string.Join(",", g.Select(o => o.Products)),
        //             CreatedAt = g.Max(o => o.CreatedAt),
        //             g.Key.UserId,
        //         });
        //     
        //     return Ok(groupedOrders);
        // }
        
        // [HttpGet]
        // public async Task<IActionResult> GetFullInformationByOrder(int userId)
        // {
        //     var user = await DatabaseContainer.User.GetOneById(userId);
        //     var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        //
        //     var groupedOrders = orders.GroupBy(o => new
        //         {
        //             o.UserId,
        //             o.DeliveryAddress
        //         })
        //         .Select(g => new
        //         {
        //             DeliveryAddress = g.Key.DeliveryAddress,
        //             UserId = g.Key.UserId,
        //
        //             Orders = g.GroupBy(o => new
        //                 {
        //                     o.Id,
        //                     o.CreatedAt
        //                 })
        //
        //                 .Select(o => new
        //                 {
        //                     OrderId = o.Key.Id,
        //                     CreatedAt = o.Key.CreatedAt,
        //
        //                     Products = o.Select(p => new
        //                     {
        //                         ProductId = p.Products,
        //                         Title = p.Products,
        //                         Description = p.Products,
        //                         Price = p.Products
        //                     }).ToList()
        //                 }).ToList()
        //         });
        //
        //     return Ok(groupedOrders);
        //
        // }
        
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