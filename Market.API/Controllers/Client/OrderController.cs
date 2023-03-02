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
    }
}