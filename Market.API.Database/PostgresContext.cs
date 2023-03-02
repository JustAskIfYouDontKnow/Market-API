using Market.API.Database.Order;
using Market.API.Database.Product;
using Market.API.Database.User;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
        }
        
        public DbSet<UserModel> User { get; set; }

        public DbSet<OrderModel> Order { get; set; }
        
        public DbSet<ProductModel> Product { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}