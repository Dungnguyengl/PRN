using System.Windows;
using ViewModel.ViewModels;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            var vm = new LoginViewModel();
            vm.PropertyChanged += OnPropertyChanged;
            DataContext = vm;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFail")
            {
                if (((LoginViewModel)DataContext).IsFail)
                {
                    new Popup
                    {
                        Title = "Login fail",
                        Message = "Email or password invalid!",
                        Button1Title = "Cancel",
                        Button2Title = "Try Again"
                    }.ShowDialog();
                }
            }
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
