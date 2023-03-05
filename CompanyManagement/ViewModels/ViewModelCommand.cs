using System;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ViewModelCommand : ICommand
    {

        // Fields
        private readonly Action<object> exeAction;
        private readonly Predicate<object> canExeAction;

        // constructor
        public ViewModelCommand(Action<object> exeAction)
        {
            this.exeAction = exeAction;
            canExeAction = null;
        }

        public ViewModelCommand(Action<object> exeAction, Predicate<object> canExeAction)
        {
            this.exeAction = exeAction;
            this.canExeAction = canExeAction;
        }

        // Events
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        // Methods
        public bool CanExecute(object? parameter)
        {
            return canExeAction == null ? true : canExeAction(parameter);
        }

        public void Execute(object? parameter)
        {
            exeAction(parameter);
        }
    }
}
