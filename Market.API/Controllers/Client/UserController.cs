using Newtonsoft.Json;
using System.Xml;
using Market.API.Database;
using Microsoft.AspNetCore.Mvc;
using Formatting = Newtonsoft.Json.Formatting;

namespace Market.API.Controllers.Client;

public class UserController : AbstractClientController
{

    public UserController(IDatabaseContainer databaseContainer) : base(databaseContainer)
    {
    }
    

    [HttpPost]
    public async Task<IActionResult> CreateUser(string firstName, string lastName)
    {
        var createdUser = await _databaseContainer.User.Create(firstName, lastName);

        return Ok(createdUser);
    }
    
    [HttpGet]
    public async Task<IActionResult> TotalOrder(int userId)
    {
        var user = await _databaseContainer.User.GetOneById(userId);
        var totalOrder = await _databaseContainer.OrderProduct.FindOrdersByUserId(user);
        return Ok(totalOrder);
    }
}