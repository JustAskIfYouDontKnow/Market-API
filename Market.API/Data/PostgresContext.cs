using Market.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.API.Data
{
    public class PostgresContext : DbContext
    {
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(250);
                entity.Property(e => e.Price).IsRequired().HasColumnType("money");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ShippingAddress).IsRequired().HasMaxLength(250);
                entity.HasMany(e => e.Products)
                    .WithMany()
                    .UsingEntity(join =>
                    {
                        join.ToTable("OrderItems");
                        join.Property<int>("OrderId");
                        join.Property<int>("ProductId");
                        join.HasKey("OrderId", "ProductId");
                    });
            });
        }
    }
}