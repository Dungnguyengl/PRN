using Model.Context;
using Model.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ViewModel.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {
        private ObservableCollection<OrderDto>? _orders;
        private OrderDto? _selectedOrder;
        private bool _isCreating = true;
        private bool _isOrderSelected;
        private ObservableCollection<MemberDto>? _memberItems;
        private ObservableCollection<ProductDto>? _productItems;
        private ProductDto? _selectedProduct;

        public ObservableCollection<OrderDto> Orders
        {
            get => _orders ??= new ObservableCollection<OrderDto>();
            set
            {
                _orders = value;
                OnPropertyChange(nameof(Orders));
            }
        }

        public OrderDto SelectedOrder
        {
            get => _selectedOrder ??= new OrderDto();
            set
            {
                _selectedOrder = value;
                IsOrderSelected = !(SelectedOrder.Id == default);
                if (IsOrderSelected)
                    FetchOrderDetail();
                OnPropertyChange(nameof(SelectedOrder));
            }
        }

        public bool IsCreating
        {
            get => _isCreating;
            set
            {
                _isCreating = value;
                OnPropertyChange(nameof(IsCreating));
            }
        }

        public bool IsOrderSelected
        {
            get => _isOrderSelected;
            set
            {
                _isOrderSelected = value;
                OnPropertyChange(nameof(IsOrderSelected));
            }
        }

        public ObservableCollection<MemberDto> MemberItems
        {
            get => _memberItems ??= new ObservableCollection<MemberDto>();
            set
            {
                _memberItems = value;
                OnPropertyChange(nameof(MemberItems));
            }
        }

        public ObservableCollection<ProductDto> ProductItems
        {
            get => _productItems ??= new ObservableCollection<ProductDto>();
            set
            {
                _productItems = value;
                OnPropertyChange(nameof(ProductItems));
            }
        }

        public ProductDto SelectedProduct
        {
            get => _selectedProduct ??= new ProductDto();
            set
            {
                _selectedProduct = value;
                if (value != null && value.Id != 0)
                {
                    OnPropertyChange(nameof(SelectedProduct));
                    AutofillProduct();
                }
            }
        }

        public OrdersViewModel()
        {
            IsLoading = true;
            FetchData();
            IsLoading = false;
        }

        private ICommand? _addCommand;

        public ICommand AddCommand => _addCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TODO: Handle add member
            if (Validate())
            {
                using var context = new DatabaseContext();
                await UpdateOrCreateOrder(context);
                FetchData();
                SelectedOrder = new();
            }
            IsLoading = false;
        });

        private ICommand? _updateCommand;

        public ICommand UpdateCommand => _updateCommand ??= new CommandBase(async () =>
        {
            //TODO: Handle update member
            IsLoading = true;
            if (Validate())
            {
                using var context = new DatabaseContext();
                await UpdateOrCreateOrder(context);
                FetchOrderDetail();
            }
            IsLoading = false;
        });

        private ICommand? _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TOTO: Handle delete member
            using var context = new DatabaseContext();
            var orderQuery = context.Orders.NoTracking().FirstOrDefault(order => order.Id == SelectedOrder.Id);
            var orderDetailIds = SelectedOrder.Details.Select(d => d.ProductId);
            var orderDetailQuery = context.OrderDetails.NoTracking()
                                                       .Where(detail => detail.OrderId == SelectedOrder.Id)
                                                       .Where(detail => orderDetailIds.Contains(detail.ProductId));
            if (orderQuery != null)
            {
                await context.OrderDetails.DeleteRangeAsync(orderDetailQuery);
                await context.SaveChangesAsync();

                await context.Orders.DeleteAsync(orderQuery);
                await context.SaveChangesAsync();
            }
            FetchData();
            IsLoading = false;
        });

        private void FetchData()
        {
            using var context = new DatabaseContext();
            Orders = _mapper.Map<IEnumerable<OrderDto>>(context.ViewGetOrders).ToObservable();

            MemberItems.FromEnumerable(_mapper.Map<IEnumerable<MemberDto>>(context.Members.NoTracking()));
        }

        private async Task UpdateOrCreateOrder(DatabaseContext context)
        {
            var newOrder = _mapper.Map<Order>(SelectedOrder);
            int id = SelectedOrder.Id;
            if (SelectedOrder.Id == 0)
            {
                await context.Orders.CreateAsync(newOrder);
                await context.SaveChangesAsync();
                id = context.Orders.OrderByDescending(order => order.Id).First().Id;
            }
            else
            {
                await context.Orders.UpdateAsync(newOrder);
            }
            await UpdateOrCreateOrDeleteOrderDetail(context, id);
            await context.SaveChangesAsync();
        }

        private async Task UpdateOrCreateOrDeleteOrderDetail(DatabaseContext context, int OrderId)
        {
            var details = _mapper.Map<IEnumerable<OrderDetail>>(SelectedOrder.Details).Where(d => d.ProductId != 0);
            var orderDetails = context.OrderDetails.NoTracking().Where(detail => detail.OrderId == SelectedOrder.Id).ToList();
            if (details != null && details.Any())
            {
                foreach (var detail in details)
                {
                    detail.OrderId = OrderId;
                    var orderDetail = orderDetails.FirstOrDefault(orderDetail => orderDetail.ProductId == detail.ProductId);
                    if (orderDetail != null)
                    {
                        context.OrderDetails.Update(detail);
                        orderDetails.Remove(orderDetail);
                    }
                    else
                    {
                        context.OrderDetails.Add(detail);
                    }
                }
            }
            if (orderDetails.Any())
            {
                await context.OrderDetails.DeleteRangeAsync(orderDetails);
                return;
            }
        }

        private void AutofillProduct()
        {
            using var context = new DatabaseContext();
            var product = _mapper.Map<OrderDetailDto>(context.Products.FirstOrDefault(p => p.Id == SelectedProduct.Id));
            var details = SelectedOrder.Details.ToList();
            var id = SelectedProduct.Id;
            var seletedProduct = details.FirstOrDefault(d => d.ProductId == id);

            if (seletedProduct == null)
            {
                product.Quantity = 1;
                details.Add(product);
            }
            else
            {
                details.Remove(seletedProduct);
                seletedProduct.Quantity++;
                seletedProduct.UnitPrice = product.UnitPrice;
                details.Add(seletedProduct);
            }

            SelectedOrder.Details.FromEnumerable(details.OrderBy(d => d.ProductId));
            FetchDropdownList();
        }

        private void FetchOrderDetail()
        {
            using var context = new DatabaseContext();
            var query = context.ViewGetOrderDetail.NoTracking()
                                                  .Where(view => view.OrderId == SelectedOrder.Id);

            var details = _mapper.Map<IEnumerable<OrderDetailDto>>(query).ToList();
            details.Add(new());

            SelectedOrder.Details.FromEnumerable(details.OrderBy(d => d.ProductId));

            SelectedOrder.Details.CollectionChanged += (object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    FetchDropdownList();
            };

            FetchDropdownList();
        }

        private void FetchDropdownList()
        {
            using var context = new DatabaseContext();
            var query = SelectedOrder.Details.Select(d => d.ProductId);
            var productQuery = context.Products.NoTracking()
            .Where(p => p.UnitInStock > 0)
                                               .Where(p => !query.Contains(p.Id));

            ProductItems.FromEnumerable(_mapper.Map<IEnumerable<ProductDto>>(productQuery));
        }

        private bool Validate()
        {
            Errors.Clear();
            //TODO: Validate here
            return !IsInvalid;
        }
    }
}
