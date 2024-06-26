namespace PizzaStoreManagement.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public DateOnly? RequiredDate { get; set; }
        public DateOnly? ShippedDate { get; set; }
        public double Frieght { get; set; }
        public string? ShipAddress { get; set; }

        // Navigation property
        public Customer? Customer { get; set; }
    }
}
