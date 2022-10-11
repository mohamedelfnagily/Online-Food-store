using Microsoft.EntityFrameworkCore;
using NahlasKitchen.Models;

namespace NahlasKitchen.Data
{
    public class AppdbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=NahlasKitchenDB;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //User Fluent api configuration
            modelBuilder.Entity<User>().Property(e => e.Role).HasDefaultValue("Customer");

            //Order configuration
            modelBuilder.Entity<Order>().Property(p => p.Date).HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<CartProduct>().HasKey(e => new { e.productId, e.CartId });
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<CartProduct> CartProduct { get; set; }
    }
}
