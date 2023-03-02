using System;
using System.Threading.Tasks;

namespace Market.API.Database.User;

public class UserRepository : AbstractRepository<UserModel>, IUserRepository
{
    public UserRepository(PostgresContext context) : base(context)
    {
    }

    public async Task<UserModel> Create(string firstName, string lastName)
    {
        var model = UserModel.CreateModel(firstName, lastName);
        
        var result = await CreateModelAsync(model);
        if (result == null)
        {
            throw new Exception("User model is not created.");
        }

        return result;
    }
}