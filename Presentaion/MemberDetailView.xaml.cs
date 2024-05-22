using Model.Entities;
using System.Windows;
using System.Windows.Controls;
using ViewModel.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MemberDetailWindow.xaml
    /// </summary>
    public partial class MemberDetailView : Window
    {
        public MemberDetailView()
        {
            InitializeComponent();
            var vm = new MemberViewModel();
            DataContext = vm;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && DataContext is MemberViewModel context)
            {
                var confirm = new Popup
                {
                    Button1Title = "Cancel",
                    Button2Title = "OK",
                };
                switch (button.Content)
                {
                    case "Confirm":
                        confirm.Title = context.IsCreating ? "Create" : "Update";
                        confirm.Message = $"Do you want {(context.IsCreating ? "create" : "update")} member?";
                        confirm.Button2Action = () =>
                        {
                            if (context.IsCreating)
                            {
                                if (context.AddCommand.CanExecute(null))
                                    context.AddCommand.Execute(null);
                            }
                            else
                            {
                                if (context.UpdateCommand.CanExecute(null))
                                    context.UpdateCommand.Execute(null);
                            }
                            context.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) =>
                            {
                                if (e.PropertyName == nameof(context.IsLoading) && !context.IsLoading)
                                    confirm.Close();
                            };
                        };
                        break;
                    case "Cancel":
                        Close();
                        return;
                }
                confirm.ShowDialog();
            }
        }
    }
}
