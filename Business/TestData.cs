using Model.Entities;
using System.ComponentModel;

namespace ViewModel
{
    public class TestData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private List<Product>? _products;
        private List<OrderDetail> _orderdetails;

        public List<Product> Products
        {
            get => _products ?? new List<Product>();
            set
            {
                _products = value;
                OnPropertyChange(nameof(Products));
            }
        }

        public List<OrderDetail> OrderDetails
        {
            get => _orderdetails ?? new List<OrderDetail>();
            set
            {
                _orderdetails = value;
                OnPropertyChange(nameof(OrderDetails));
            }
        }

        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
