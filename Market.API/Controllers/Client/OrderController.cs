using Market.API.Client.Payload;
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
        public async Task<IActionResult> GetFullOrderInformation(int userId, int orderId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
           
            
            var order = await DatabaseContainer.Order.GetOneById(orderId);
            if (user.Id != order.UserId)
            {
                return BadRequest($"User ID: {user.Id} tried to get full information for order ID: {order.Id}. Cannot complete this operation because it is not a user's order.");
            }

            var result = new OrderDetails
            {
                OrderId = order.Id,
                UserId = order.UserId,
                DeliveryAddress = order.DeliveryAddress,
                CreatedAt = order.CreatedAt,
                ProductsInOrder = order.OrderProducts.Select(pd => new ProductDetails
                {
                    Id = pd.ProductModel.Id,
                    Description = pd.ProductModel.Description,
                    Title = pd.ProductModel.Title,
                    Price = pd.ProductModel.Price
                }).ToList(),
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFullOrdersInformation(int userId)
        {
            var user = await DatabaseContainer.User.GetOneById(userId);
            var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);

            var result = orders.Select(od => new OrderDetails
            {
                OrderId = od.Id,
                UserId = od.UserId,
                DeliveryAddress = od.DeliveryAddress,
                CreatedAt = od.CreatedAt,
                ProductsInOrder = od.OrderProducts.Select(pd => new ProductDetails
                {
                    Id = pd.ProductModel.Id,
                    Description = pd.ProductModel.Description,
                    Title = pd.ProductModel.Title,
                    Price = pd.ProductModel.Price
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