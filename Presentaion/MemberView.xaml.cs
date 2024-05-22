using System.Windows;
using System.Windows.Controls;
using ViewModel.ViewModels;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MemberView.xaml
    /// </summary>
    public partial class MemberView : Window
    {
        public MemberView()
        {
            InitializeComponent();
            DataContext = new MemberViewModel();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var memberDetailView = new MemberDetailView();
            var vm = (MemberViewModel)DataContext;

            switch (button?.Content)
            {
                case "Add":
                    memberDetailView.ShowDialog();
                    break;
                case "Modify":
                    memberDetailView.ShowDialog();
                    break;
                case "Delete":
                    var confirm = new Popup
                    {
                        Title = "Delete!",
                        Message = "Do you want delete this member?",
                        Button1Title = "Cancel",
                        Button1Action = Close,
                        Button2Title = "Delete",
                        Button2Action = () =>
                        {
                            if (vm?.DeleteCommand.CanExecute(null) ?? false)
                                vm.DeleteCommand.Execute(null);
                        }
                    };
                    break;
                default:
                    break;
            }
        }
    }
}
