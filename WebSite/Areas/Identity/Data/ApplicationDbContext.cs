using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSite.Areas.Identity.Data;
using WebSite.Models;

namespace WebSite.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderStatus> OrderStatus { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<FavouriteProducts> FavouriteProducts { get; set; }
    public DbSet<LastVisited> LastVisitedsProducts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>()
         .HasMany(u => u.FavouriteProducts)
         .WithOne(f => f.User);

        modelBuilder.Entity<ApplicationUser>()
        .HasMany(u => u.LastVisitedProducts)
        .WithOne(f => f.User);

        modelBuilder.Entity<ClothingVariant>()
               .HasKey(v => v.Id);
        modelBuilder.Entity<Product>()
            .HasOne(o => o.Category)
            .WithMany(s => s.Products)
            .HasForeignKey(o => o.CategoryId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductId);

        modelBuilder.Entity<ClothingVariant>()
           .HasOne(v => v.Product)
           .WithMany(p => p.Variants)
           .HasForeignKey(v => v.ProductId);


    }
}

