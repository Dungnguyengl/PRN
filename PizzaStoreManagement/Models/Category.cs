namespace PizzaStoreManagement.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        // Navigation property
        public Products? Products { get; set; }

    }
}