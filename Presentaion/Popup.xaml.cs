using System.ComponentModel;
using System;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class Popup : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _title;
        private string? _message;
        private string? _button1Title;
        private string? _button2Title;
        private Action? _button1Action;
        private Action? _button2Action;

        private void DefaulAction () 
        {
            Close();
        }

        public string PopupTitle
        {
            get => _title ?? "Title";
            set
            {
                _title = value;
                OnPropertyChanged(nameof(PopupTitle));
            }
        }

        public string Message
        {
            get => _message ?? "Message";
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public string Button1Title
        {
            get => _button1Title ?? "Button1";
            set
            {
                _button1Title = value;
                OnPropertyChanged(nameof(Button1Title));
            }
        }

        public string Button2Title
        {
            get => _button2Title ?? "Button2";
            set
            {
                _button2Title = value;
                OnPropertyChanged(nameof(Button2Title));
            }
        }

        public Action Button1Action
        {
            get => _button1Action ?? DefaulAction;
            set => _button1Action = value;
        }

        public Action Button2Action
        {
            get => _button2Action ?? DefaulAction;
            set => _button2Action = value;
        }

        public Popup()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button1Action.Invoke();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button2Action.Invoke();
        }
    }
}
