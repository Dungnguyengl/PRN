using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using ViewModel.Dtos;

namespace ViewModel
{
    public static class Extentions
    {
        public static ObservableCollection<T> FromEnumerable<T>(this ObservableCollection<T> collection, IEnumerable<T> enumerable, Action<string?>? action = null)
        {
            collection.Clear();
            foreach (var item in enumerable)
            {
                if (item is NotifyDtoBase i)
                {
                    i.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) =>
                    {
                        action?.Invoke(e.PropertyName);
                    };
                }
                collection.Add(item);
            }
            return collection;
        }

        public static ObservableCollection<T> FromEnumerable<T>(this ObservableCollection<T> collection, IEnumerable<T> enumerable)
        {
            return collection.FromEnumerable(enumerable, null);
        }

        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection)
        {
            var newCollection = new ObservableCollection<T>();
            return newCollection.FromEnumerable(collection);
        }

        public static bool IsNullOrEmpty(this string? obj)
        {
            return obj == null || obj == string.Empty;
        }

        public static bool IsMatchRegex(this string? str, string pattern)
        {
            var regex = new Regex(pattern);
            return !str.IsNullOrEmpty() && regex.IsMatch(str);
        }
    }
}
