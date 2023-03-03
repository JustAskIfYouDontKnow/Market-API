using System.Collections.Generic;
using System.Threading.Tasks;

namespace Market.API.Database.User;

public interface IUserRepository
{
    Task<UserModel> Create(string firstName, string lastName);

    Task<UserModel> GetOneById(int id);

    Task<List<UserModel>> GetAllUsers();
}