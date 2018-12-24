
namespace ShopingCartDemo.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ShopingCartDemo.Data.Models;
    using ShopingCartDemo.Models;

    public class ShopingCartDbContext : IdentityDbContext<ApplicationUser>
    {
        public ShopingCartDbContext(DbContextOptions<ShopingCartDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProductItem> OrderProductItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(u => u.UserId);

            builder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(opi => opi.Order)
                .HasForeignKey(u => u.OrderId);

            base.OnModelCreating(builder);
        }
    }
}
