using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public MemberDto Member { get; set; } = new MemberDto();
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime RequiredDate { get; set; } = DateTime.Now;
        public DateTime ShippedDate { get; set;} = DateTime.Now;
        public decimal Freight { get; set; }
        public ObservableCollection<OrderDetailDto> Details { get; set; } = new ObservableCollection<OrderDetailDto>();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public float Discount { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
    }
}
