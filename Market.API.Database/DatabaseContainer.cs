using Market.API.Database.Order;
using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.Services;
using Market.API.Database.User;

namespace Market.API.Database;

public class DatabaseContainer : IDatabaseContainer
{
    public IUserRepository User { get; set; }

    public IProductRepository Product { get; set; }

    public IOrderModelRepository Order { get; set; }

    public IOrderProductRepo OrderProduct { get; set; }

    public OrderService OrderService { get; set; }

    public ServiceContainer ServiceContainer { get; }


    public DatabaseContainer(PostgresContext db)
    {
        User = new UserRepository(db);
        Product = new ProductRepository(db);
        Order = new OrderModelRepository(db);

        OrderService = new OrderService(User, Product, Order);

        ServiceContainer = new ServiceContainer(OrderService);
    }
}