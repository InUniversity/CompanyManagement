using System;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ReplayCommand<T> : ICommand
    {

        private readonly Action<T> exeAction;
        private readonly Predicate<T> canExeAction;

        public ReplayCommand(Action<T> exeAction)
        {
            this.exeAction = exeAction;
            canExeAction = null;
        }

        public ReplayCommand(Action<T> exeAction, Predicate<T> canExeAction)
        {
            this.exeAction = exeAction;
            this.canExeAction = canExeAction;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            try
            {
                return canExeAction == null ? true : canExeAction((T)parameter);
            } 
            catch
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            exeAction((T)parameter);
        }
    }
}
