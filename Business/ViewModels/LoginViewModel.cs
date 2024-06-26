using Microsoft.EntityFrameworkCore;
using Model.Context;
using System.Windows.Input;

namespace ViewModel.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string? _email;
        private string? _password;
        private bool _isFail;
        private bool _isSuccess;

        public string Email
        {
            get => _email ?? string.Empty;
            set
            {
                _email = value;
                OnPropertyChange(nameof(Email));
            }
        }

        public string Password
        {
            get => _password ?? string.Empty;
            set
            {
                _password = value;
                OnPropertyChange(nameof(Password));
            }
        }

        public bool IsFail
        {
            get => _isFail;
            set 
            {
                _isFail = value;
                OnPropertyChange(nameof(IsFail));
            }
        }

        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                _isSuccess = value;
                OnPropertyChange(nameof(IsSuccess));
            }
        }

        private ICommand? _loginCommand;

        public ICommand LoginCommand => _loginCommand ??= new CommandBase(() =>
        {
            using var context = new DatabaseContext();
            var user = context.Members.AsNoTracking()
                .FirstOrDefault(member => member.Email == Email && member.Password == Password);
            if (user == null)
            {
                IsFail = true;
            }
            else
            {
                IsSuccess = true;
            }
        });
    }
}
