using AutoMapper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModel.Dtos;

namespace ViewModel.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        private bool _isLoading;
        private ObservableCollection<ErrorDto>? _errors;
        private bool _isInvalid;

        protected readonly Mapper _mapper;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChange(nameof(IsLoading));
            }
        }

        public ObservableCollection<ErrorDto> Errors
        {
            get => _errors ??= new ObservableCollection<ErrorDto>();
            set
            {
                _errors = value;
                OnPropertyChange(nameof(Errors));
            }
        }

        public bool IsInvalid
        {
            get => _isInvalid;
            set
            {
                _isInvalid = value;
                OnPropertyChange(nameof(_isInvalid));
            }
        }

        public ViewModelBase()
        {
            var mapperCfg = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Profiles>();
            });
            _mapper = new Mapper(mapperCfg);
            Errors.CollectionChanged += Errors_CollectionChanged;
        }

        private void Errors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IsInvalid = Errors.Any();
        }
    }
}
