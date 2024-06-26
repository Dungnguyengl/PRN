using System.ComponentModel.DataAnnotations;

namespace PizzaStoreManagement.Models
{
    public class Supplier
    {
        public int SupplerId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        [Phone]
        public string? Phone { get; set; }
        // Navigation property
        public Products? Products { get; set; }
    }
}