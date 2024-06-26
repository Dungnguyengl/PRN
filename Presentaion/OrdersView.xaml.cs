using System.Windows;
using System.Windows.Controls;
using ViewModel.ViewModels;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrdersView.xaml
    /// </summary>
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();
            var vm = new OrdersViewModel();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var vm = (OrdersViewModel)DataContext;

            switch (button?.Content)
            {
                case "Add":
                    ShowMemberPopup(vm, true);
                    break;
                case "Detail":
                    ShowMemberPopup(vm, false);
                    break;
                case "Delete":
                    var confirm = new Popup
                    {
                        Title = "Delete!",
                        Message = "Do you want delete this order?",
                        Button1Title = "Cancel",
                        Button2Title = "Delete"
                    };
                    confirm.Button2Action = () =>
                    {
                        if (vm?.DeleteCommand.CanExecute(null) ?? false)
                            vm.DeleteCommand.Execute(null);
                        vm.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) =>
                        {
                            if (e.PropertyName == nameof(vm.IsLoading) && !vm.IsLoading)
                                confirm.Close();
                        };
                    };
                    confirm.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void ShowMemberPopup(OrdersViewModel vm, bool isCreating)
        {
            if (isCreating)
                vm.SelectedOrder = new()
                {
                    Details = new(){new()}
                };
            vm.IsCreating = isCreating;

            var memberDetailView = new OrderDetailView
            {
                DataContext = vm
            };
            memberDetailView.ShowDialog();
        }
    }
}
