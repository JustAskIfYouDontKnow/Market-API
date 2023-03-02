using Market.API.Database.Order;
using Market.API.Database.Product;
using Market.API.Database.User;

namespace Market.API.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; set; }
    public IOrderRepository Order { get; set; }
    public IProductRepository Product { get; set; }


    public DatabaseContainer(PostgresContext db)
    {
        User = new UserRepository(db);
        Order = new OrderRepository(db);
        Product = new ProductRepository(db);
    }
}