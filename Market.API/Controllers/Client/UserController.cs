using Market.API.Database;
using Market.API.Database.User;
using Microsoft.AspNetCore.Mvc;


namespace Market.API.Controllers.Client;

public class UserController : AbstractClientController
{

    public UserController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }


    [HttpPost]
    public async Task<IActionResult> CreateUser(string firstName, string lastName)
    {
        var createdUser = await DatabaseContainer.User.Create(firstName, lastName);
        return Ok(createdUser);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var collection = await DatabaseContainer.User.GetAllUsers();
        return Ok(collection);
    }
    
    public async Task<IActionResult> LoginUser([FromForm] string username, [FromForm] string password)
    {
        Console.WriteLine("User " + username + "Password " + password);
        return Ok();
    }

}