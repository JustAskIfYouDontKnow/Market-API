using Market.API.Client.Payload;
using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers.Client;

public class UserInformationController: AbstractClientController
{
    public UserInformationController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }
    
    [HttpGet("user-product-count")]
    public async Task<IActionResult> GetUserProductCount(int userId, int productId)
    {
        var user = await DatabaseContainer.User.GetOneById(userId);
        var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        var productCount = orders.Sum(o => o.OrderProducts.Count(op => op.ProductModel.Id == productId));
        return Ok(productCount);
    }
    
    
    [HttpGet("user-order-count")]
    public async Task<IActionResult> GetUserOrderCount(int userId)
    {
        var user = await DatabaseContainer.User.GetOneById(userId);
        var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        var productCount = orders.Sum(o => o.OrderProducts.Count);
        return Ok(productCount);
    }
    
    [HttpGet("user-product-ids-count-in-orders")]
    public async Task<IActionResult> GetUserProductsInOrdersCount(int userId)
    {
        var user = await DatabaseContainer.User.GetOneById(userId);
        var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);

        var productOrderCounts = orders
            .SelectMany(o => o.OrderProducts)
            .GroupBy(op => op.ProductModel.Id)
            .ToDictionary(g => g.Key, g => g.Count());

        var response = new
        {
            UserId = user.Id,
            ProductOrderCounts = productOrderCounts
        };

        return Ok(response);
    }
   





    
    [HttpGet]
    public async Task<IActionResult> GetGroupedOrdersByDeliveryAddress(int userId)
    {
        var user = await DatabaseContainer.User.GetOneById(userId);
        var orders = await DatabaseContainer.Order.FindOrdersByUserId(user);
        
        var groupedOrders = orders
            .GroupBy(o => new { o.UserId, o.DeliveryAddress })
            .Select(g => new OrderDetails
            {
                UserId = g.Key.UserId,
                DeliveryAddress = g.Key.DeliveryAddress,
                CreatedAt = g.Max(o => o.CreatedAt),
                ProductsInOrder = g.SelectMany(o => o.OrderProducts.Select(op => new ProductDetails
                {
                    Id = op.ProductModel.Id,
                    OrderId = op.OrderId,
                    Title = op.ProductModel.Title,
                    Description = op.ProductModel.Description,
                    Price = op.ProductModel.Price
                })).ToList()
            });

        return Ok(groupedOrders);
    }
}