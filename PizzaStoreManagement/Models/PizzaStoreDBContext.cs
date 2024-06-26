using Microsoft.EntityFrameworkCore;

namespace PizzaStoreManagement.Models
{
    public class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext(DbContextOptions<PizzaStoreDBContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Accounts> Account { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
           .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Orders>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Orders>()
                .Property(o => o.OrderId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Order)
                .WithOne(o => o.Customer)
                .HasForeignKey<Orders>(o => o.CustomerId);

            modelBuilder.Entity<Customer>()
           .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Accounts>()
                .Property(c => c.AccountId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Products>()
                .Property(c => c.ProductId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Supplier>()
                .Property(c => c.SupplerId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Products>()
                .HasKey(o => o.ProductId);

            modelBuilder.Entity<Accounts>()
                .HasKey(o => o.AccountId);

            modelBuilder.Entity<Supplier>()
                .HasKey(o => o.SupplerId);

            modelBuilder.Entity<Category>()
                .HasKey(o => o.CategoryId);

            modelBuilder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            modelBuilder.Entity<OrderDetails>()
                .HasOne(od => od.Products)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Products>()
                .HasIndex(p => p.SupplierId)
                .IsUnique(false); // This ensures the index is not unique

            modelBuilder.Entity<Products>()
                .HasIndex(p => p.CategoryId)
                .IsUnique(false); // This ensures the index is not unique
        }
    }
}
