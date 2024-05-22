using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Model.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ViewGetOrderDetail> ViewGetOrderDetail { get; set; }
        public DbSet<ViewGetOrder> ViewGetOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SaleManager;User Id=admin;Password=admin;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Member>(member =>
            {
                member.ToTable("MEMBER");
                member.Property(e => e.Id)
                .HasColumnName("MemberId");
            });
            modelBuilder.Entity<Order>(order =>
            {
                order.ToTable("ORDER");
                order.Property(e => e.Id)
                .HasColumnName("OrderId");
            });
            modelBuilder.Entity<Product>(product =>
            {
                product.ToTable("PRODUCT");
                product.Property(e => e.Id)
                .HasColumnName("ProductId");
            });
            modelBuilder.Entity<OrderDetail>(orderDetail =>
            {
                orderDetail.ToTable("ORDER_DETAIL");
                orderDetail.HasKey(e => new {e.ProductId, e.OrderId});
            });
            modelBuilder.Entity<ViewGetOrderDetail>(viewGetOrderDetai =>
            {
                viewGetOrderDetai.ToView("GET_ORDER_DETAIL");
                viewGetOrderDetai.HasNoKey();
            });
            modelBuilder.Entity<ViewGetOrder>(viewGetOrder =>
            {
                viewGetOrder.ToView("GET_ORDER");
                viewGetOrder.HasNoKey();
            });
        }
    }
}
