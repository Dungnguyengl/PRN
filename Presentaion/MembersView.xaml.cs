using System.Windows;
using System.Windows.Controls;
using ViewModel.ViewModels;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MembersView.xaml
    /// </summary>
    public partial class MembersView : Page
    {
        public MembersView()
        {
            InitializeComponent();
            var vm = new MemberViewModel();
            DataContext = vm;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var vm = (MemberViewModel)DataContext;

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
                        Message = "Do you want delete this member?",
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

        private void ShowMemberPopup(MemberViewModel vm, bool isCreating)
        {
            if (isCreating)
                vm.SelectedMember = new();
            vm.IsCreating = isCreating;
            var memberDetailView = new MemberDetailView
            {
                DataContext = vm
            };
            memberDetailView.ShowDialog();
        }
    }
}
