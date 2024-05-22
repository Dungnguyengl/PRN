using SalesWPFApp;
using System.Windows;

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
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            new LoginView().ShowDialog();
        }

        private void MemberClick(object sender, RoutedEventArgs e)
        {
            new MemberView().ShowDialog();
        }

        private void ProductClick(object sender, RoutedEventArgs e)
        {
            new ProductionView().ShowDialog();
        }

        private void OrderClick(object sender, RoutedEventArgs e)
        {
            new OrderView().ShowDialog();
        }
    }
}
