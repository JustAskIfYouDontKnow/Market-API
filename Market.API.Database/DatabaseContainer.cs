using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.Services;
using Market.API.Database.User;

namespace Market.API.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; set; }
    public IProductRepository Product { get; set; }
    public IOrderProductModelRepo OrderProduct { get; set; }
    
    public OrderService OrderService { get; set; }


    public DatabaseContainer(PostgresContext db)
    {
        User = new UserRepository(db);
        Product = new ProductRepository(db);
        OrderProduct = new OrderProductModelRepo(db);
        OrderService = new OrderService(
            new UserRepository(db),
            new ProductRepository(db),
            new OrderProductModelRepo(db));
    }
}