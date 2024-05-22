using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SalesWPFApp
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (bool)value;
            return data ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}
