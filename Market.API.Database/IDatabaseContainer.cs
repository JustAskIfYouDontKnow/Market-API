using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.Services;
using Market.API.Database.User;

namespace Market.API.Database;

public interface IDatabaseContainer
{
    public IUserRepository User { get; set; }
    public IProductRepository Product { get; set; }
    public IOrderProductModelRepo OrderProduct { get; set; }
    
    public OrderService OrderService { get; set; }
}