namespace PizzaStoreManagement.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }    
        public int? QuantityPerUnit { get; set; }
        public double UnitPrice { get; set; }
        public string? ProductImage { get; set; }

        // Navigation property
        public Supplier? Supplier { get; set; }
        public Category? Category { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }

    }
}
