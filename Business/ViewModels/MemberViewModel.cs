using Model.Context;
using Model.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Dtos;

namespace ViewModel.ViewModels
{
    public class MemberViewModel : ViewModelBase
    {
        private ObservableCollection<Member>? _members;
        private Member? _selectedMember;
        private bool _isCreating = true;
        private bool _isMemberSelected;

        public ObservableCollection<Member> Members
        {
            get => _members ??= new ObservableCollection<Member>();
            set
            {
                _members = value;
                OnPropertyChange(nameof(Members));
            }
        }

        public Member SelectedMember
        {
            get => _selectedMember ??= new Member();
            set
            {
                _selectedMember = value;
                IsMemberSelected = !(SelectedMember.Id == default);
                OnPropertyChange(nameof(SelectedMember));
            }
        }

        public bool IsCreating
        {
            get => _isCreating;
            set
            {
                _isCreating = value;
                OnPropertyChange(nameof(IsCreating));
            }
        }

        public bool IsMemberSelected
        {
            get => _isMemberSelected;
            set
            {
                _isMemberSelected = value;
                OnPropertyChange(nameof(IsMemberSelected));
            }
        }

        public MemberViewModel()
        {
            IsLoading = true;
            Reload();
            IsLoading = false;
        }

        private ICommand? _addCommand;

        public ICommand AddCommand => _addCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TODO: Handle add member
            if (Validate())
            {
                using var context = new DatabaseContext();
                await context.Members.CreateAsync(SelectedMember);
                await context.SaveChangesAsync();
                Reload();
                SelectedMember = new();
            }
            IsLoading = false;
        });

        private ICommand? _updateCommand;

        public ICommand UpdateCommand => _updateCommand ??= new CommandBase(async () =>
        {
            //TODO: Handle update member
            IsLoading = true;
            if (Validate())
            {
                using var context = new DatabaseContext();
                await context.Members.UpdateAsync(SelectedMember);
                await context.SaveChangesAsync();
                var currentMember = SelectedMember;
                Reload();
                SelectedMember = currentMember;
            }
            IsLoading = false;
        });

        private ICommand? _deleteCommand;

        public ICommand DeleteCommand => _deleteCommand ??= new CommandBase(async () =>
        {
            IsLoading = true;
            //TOTO: Handle delete member
            using var context = new DatabaseContext();
            await context.Members.DeleteAsync(SelectedMember);
            await context.SaveChangesAsync();
            Reload();
            IsLoading = false;
        });

        private void Reload()
        {
            using var context = new DatabaseContext();
            Members.FromEnumerable(context.Members.AsEnumerable());
        }

        private bool Validate()
        {
            Errors.Clear();
            var email = SelectedMember.Email;
            if (email.IsNullOrEmpty() || !email.IsMatchRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                Errors.Add(new ErrorDto { FieldName = "Email", Message = "Email is invalid" });
            return !IsInvalid;
        }
    }
}
