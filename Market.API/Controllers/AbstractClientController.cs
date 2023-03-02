using Market.API.Database;
using Microsoft.AspNetCore.Mvc;

namespace Market.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AbstractClientController : ControllerBase
{
    protected readonly IDatabaseContainer _databaseContainer;

    protected AbstractClientController(IDatabaseContainer databaseContainer)
    {
        _databaseContainer = databaseContainer;
    }
}