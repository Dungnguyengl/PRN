using System.ComponentModel;

namespace ViewModel.Dtos
{
    public class NotifyDtoBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }
    }
}
