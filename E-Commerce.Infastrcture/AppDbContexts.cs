using System;
using E_Commerce.Domain.Entites.AppIdentity;
using E_Commerce.Domain.Entities.AppEntitie;
using E_Commerce.Domain.Entities.AppIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace E_Commerce.Infastrcture;

public class AppDbContexts : IdentityDbContext<Account>
{
    public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
    {
    }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Explicitly configure Admin and Customer entities
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Customer>().ToTable("Customers");

           
            
          
            
            // Customer -> Orders (one-to-many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Customer -> Reviews (one-to-many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Customer -> Cart (one-to-one)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Customer)
                .WithOne(c => c.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Customer -> Addresses (one-to-many)
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Customer -> WishlistItems (one-to-many)
            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Customer)
                .WithMany(c => c.WishlistItems)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
      
                
            // Category -> Products (one-to-many)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Product -> ProductImages (one-to-many)
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Product -> ProductVariants (one-to-many)
            modelBuilder.Entity<ProductVariant>()
                .HasOne(pv => pv.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(pv => pv.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Product -> Reviews (one-to-many)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Cart -> CartItems (one-to-many)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // CartItem -> Product (many-to-one)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // CartItem -> ProductVariant (many-to-one, optional)
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariant)
                .WithMany()
                .HasForeignKey(ci => ci.ProductVariantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Order -> OrderItems (one-to-many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // OrderItem -> Product (many-to-one)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // OrderItem -> ProductVariant (many-to-one, optional)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductVariant)
                .WithMany()
                .HasForeignKey(oi => oi.ProductVariantId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Order -> StatusHistory (one-to-many)
            modelBuilder.Entity<OrderStatusHistory>()
                .HasOne(osh => osh.Order)
                .WithMany(o => o.StatusHistory)
                .HasForeignKey(osh => osh.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Order -> Address (many-to-one, for shipping)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Order -> Address (many-to-one, for billing)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.BillingAddress)
                .WithMany()
                .HasForeignKey(o => o.BillingAddressId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Product -> WishlistItems (one-to-many)
            modelBuilder.Entity<WishlistItem>()
                .HasOne(w => w.Product)
                .WithMany(p => p.WishlistItems)
                .HasForeignKey(w => w.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Add indexes for better performance
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.SKU)
                .IsUnique();
                
            modelBuilder.Entity<Coupon>()
                .HasIndex(c => c.Code)
                .IsUnique();
                
            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderNumber)
                .IsUnique();
                
            // Configure precision for decimal properties in Coupon entity
            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.MinimumOrderAmount).HasColumnType("decimal(18,2)");
            });

            // Ensure all decimal properties have consistent precision
            modelBuilder.Entity<Order>(entity => {
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.ShippingAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<OrderItem>(entity => {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Cart>(entity => {
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<CartItem>(entity => {
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Product>(entity => {
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
                entity.Property(e => e.DiscountPrice).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<ProductVariant>(entity => {
                entity.Property(e => e.AdditionalPrice).HasColumnType("decimal(18,2)");
            });
        
        }
    

}
