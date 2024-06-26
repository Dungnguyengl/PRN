using System.ComponentModel.DataAnnotations;

namespace PizzaStoreManagement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? ContactName { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        [Phone]
        public string? Phone { get; set; }

        // Navigation property
        public Orders? Order { get; set; }
    }
}
