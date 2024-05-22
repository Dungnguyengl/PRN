using System.Windows;
using System.Windows.Controls;
using ViewModel.ViewModels;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : Page
    {
        public ProductsView()
        {
            InitializeComponent();
            var vm = new ProductViewModel();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var vm = (ProductViewModel)DataContext;

            switch (button?.Content)
            {
                case "Add":
                    ShowMemberPopup(vm, true);
                    break;
                case "Modify":
                    ShowMemberPopup(vm, false);
                    break;
                case "Delete":
                    var confirm = new Popup
                    {
                        Title = "Delete!",
                        Message = "Do you want delete this product?",
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

        private void ShowMemberPopup(ProductViewModel vm, bool isCreating)
        {
            if (isCreating)
                vm.SelectedProduct = new();
            vm.IsCreating = isCreating;
            var memberDetailView = new ProductionDetailView
            {
                DataContext = vm
            };
            memberDetailView.ShowDialog();
        }
    }
}
