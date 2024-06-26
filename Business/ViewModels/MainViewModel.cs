using ViewModel.Dtos;

namespace ViewModel.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string? _welcomeTile;
        private bool _isLogin;
        private bool _isAdmin;
        private CurentUserDto? _curentUser;

        public string WelcomeTile
        {
            get => _welcomeTile ??= "Welcome, Anonymus";
            set
            {
                _welcomeTile = value;
                OnPropertyChange(nameof(WelcomeTile));
            }
        }

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChange(nameof(IsLogin));
            }
        }

        public bool IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                OnPropertyChange(nameof(IsAdmin));
            }
        }

        public CurentUserDto? CurentUser
        {
            get => _curentUser;
            set
            {
                _curentUser = value;
                OnPropertyChange(nameof(CurentUser));
            }
        }
    }
}
