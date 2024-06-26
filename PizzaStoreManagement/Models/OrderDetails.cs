using System.ComponentModel.DataAnnotations;

namespace PizzaStoreManagement.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderId { get; set; }

        [Key]
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }

        // Navigation property
        public Orders? Order { get; set; }
        public Products? Products { get; set; }

    }
}
