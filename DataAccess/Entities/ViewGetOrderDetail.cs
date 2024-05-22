namespace Model.Entities
{
    public class ViewGetOrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId {get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
