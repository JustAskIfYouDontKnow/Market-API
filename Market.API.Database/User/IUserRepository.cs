using System.Threading.Tasks;

namespace Market.API.Database.User;

public interface IUserRepository
{
    Task<UserModel> Create(string firstName, string lastName);
}