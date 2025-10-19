using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TMDT.DAL
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=TMDTEntities")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderTbl> OrderTbls { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasMany(e => e.OrderTbls)
                .WithOptional(e => e.Address)
                .HasForeignKey(e => e.BillingAddressID);

            modelBuilder.Entity<Address>()
                .HasMany(e => e.OrderTbls1)
                .WithOptional(e => e.Address1)
                .HasForeignKey(e => e.ShippingAddressID);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CartItem>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Category1)
                .WithOptional(e => e.Category2)
                .HasForeignKey(e => e.ParentCategoryID);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.DefaultCategoryID);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.Value)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Coupon>()
                .Property(e => e.MinOrderAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.OrderTbls)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderItem>()
                .Property(e => e.TotalPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .Property(e => e.SubTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .Property(e => e.ShippingFee)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .Property(e => e.TaxAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .Property(e => e.DiscountAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .Property(e => e.TotalAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderTbl>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.OrderTbl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderTbl>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.OrderTbl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderTbl>()
                .HasMany(e => e.Shipments)
                .WithRequired(e => e.OrderTbl)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductVariants)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductVariant>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.ProductVariant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(e => e.Inventories)
                .WithRequired(e => e.ProductVariant)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductVariant>()
                .HasMany(e => e.OrderItems)
                .WithRequired(e => e.ProductVariant)
                .WillCascadeOnDelete(false);
        }
    }
}
