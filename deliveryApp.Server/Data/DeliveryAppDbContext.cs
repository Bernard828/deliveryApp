using deliveryApp.Server.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace deliveryApp.Server.Data
{
    public class DeliveryAppDbContext : DbContext
    {
        public DeliveryAppDbContext(DbContextOptions<DeliveryAppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Restaurant> Restaurants { get; set; } = null!;
        public DbSet<CuisineType> CuisineTypes { get; set; } = null!;
        public DbSet<RestaurantHour> RestaurantHours { get; set; } = null!;
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints if needed

            //Price precision settings
            modelBuilder.Entity<MenuItem>()
             .Property(m => m.Price)
             .HasPrecision(18, 2);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<OrderItem>()
           .Property(oi => oi.Price)
           .HasPrecision(18, 2);

            //User & Role Relationships
            modelBuilder.Entity<User>()
                 .HasOne(u => u.Role)
                 .WithMany(r => r.Users)
                 .HasForeignKey(u => u.RoleId)
                 .OnDelete(DeleteBehavior.Restrict);

            //Restaurant, Cuisine, and Hours Relationships
            modelBuilder.Entity<Restaurant>()
             .HasMany(r => r.MenuItems)
             .WithOne(m => m.Restaurant)
             .HasForeignKey(m => m.RestaurantId)
              .OnDelete(DeleteBehavior.Cascade);

            //Order & OrderItem Relationships
            modelBuilder.Entity<Order>()
             .HasOne(o => o.Customer)
             .WithMany(u => u.Orders)
             .HasForeignKey(o => o.CustomerId)
             .HasPrincipalKey(u => u.UserId);


            modelBuilder.Entity<Order>()
                .HasOne(o => o.Driver)
                .WithMany()
                .HasForeignKey(o => o.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany()
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
