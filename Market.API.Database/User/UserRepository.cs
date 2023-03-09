using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database.User;

public class UserRepository : AbstractRepository<UserModel>, IUserRepository
{
    public UserRepository(PostgresContext context) : base(context) { }


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


    public async Task<UserModel> GetOneById(int id)
    {
        var user = await DbModel.FindAsync(id);

        if (user is null)
        {
            throw new Exception($"User ID: {id} is not found");
        }

        return user;
    }
    
    public async Task<List<UserModel>> GetAllUsers()
    {
        return await DbModel.ToListAsync();
    }
}