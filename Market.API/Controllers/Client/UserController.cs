using Market.API.Database;
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

}