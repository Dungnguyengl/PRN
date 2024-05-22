namespace Model.Entities
{
    public class Product : EntityBase
    {
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public string? Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitInStock { get; set; }
    }
}
