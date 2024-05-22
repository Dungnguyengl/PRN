using System.Windows.Input;

namespace ViewModel
{
    public class CommandBase : ICommand
    {
        private Action<object?>? execute;

        private Predicate<object?>? canExecute;

        private event EventHandler? CanExecuteChangedInternal;

        public CommandBase(Action<object?> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            canExecute = (res) => true;
        }

        public CommandBase(Action execute)
        {
            this.execute = (object? res) => 
            { 
                if (execute == null)
                {
                   throw new ArgumentNullException(nameof(execute));
                }
                execute();
            };
            canExecute = (res) => true;
        }

        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
             return canExecute?.Invoke(parameter) ?? false;
        }

        public void Execute(object? parameter)
        {
                execute?.Invoke(parameter);
        }

        public void OnCanExecuteChanged()
        {
            CanExecuteChangedInternal?.Invoke(this, EventArgs.Empty);
        }

        public void Destroy()
        {
            canExecute = _ => false;
            execute = _ => { return; };
        }
    }
}
