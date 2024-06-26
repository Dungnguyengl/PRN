using SalesWPFApp;
using System.Windows;
using ViewModel.ViewModels;

namespace Presentaion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            DataContext = vm;
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var view = new LoginView();
            view.ShowDialog();
            if (view.DataContext is LoginViewModel loginViewModel && DataContext is MainViewModel mainViewModel)
            {
                mainViewModel.WelcomeTile = $"Welcome, {loginViewModel.Email}";
                mainViewModel.IsLogin = true ;
            }
        }
    }
}
