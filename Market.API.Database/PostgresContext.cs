using Market.API.Database.OrderProduct;
using Market.API.Database.Product;
using Market.API.Database.User;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Database
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options) { }

        public DbSet<UserModel> User { get; set; }

        public DbSet<ProductModel> Product { get; set; }

        public DbSet<OrderProductModel> OrderProduct { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}