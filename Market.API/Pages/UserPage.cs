using Market.API.Controllers.Client;
using Market.API.Database;
using Market.API.Database.Product;
using Market.API.Database.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Market.API.Pages;

public class UserPage : PageModel
{
    private readonly IDatabaseContainer _databaseContainer;
    private readonly UserController _userController;

    public UserPage(IDatabaseContainer databaseContainer, UserController userController)
    {
        _databaseContainer = databaseContainer;
        _userController = userController;
    }

    public List<UserModel> Users { get; set; }

    public async Task OnGetAsync()
    {
        var result = await _userController.GetAllUsers() as OkObjectResult;
        Users = result?.Value as List<UserModel>;
    }


    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostAsync(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return BadRequest("First name and last name are required");
        }

        var result = await _userController.CreateUser(firstName, lastName);

        if (result is BadRequestObjectResult badRequestResult)
        {
            return BadRequest(badRequestResult.Value);
        }

        return RedirectToPage();
    }
}