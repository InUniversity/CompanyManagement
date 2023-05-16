using System;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.Services
{
    public class AlertDialogService
    {
        private AlertDialog alertDialog = new AlertDialog();
        private AlertDialogViewModel viewmodel;

        private Action yesAction;
        private Action noAction;

        public AlertDialogService(string title, string message, Action yesAction, Action noAction)
        {
            viewmodel = alertDialog.ViewModel;
            viewmodel.Title = title;
            viewmodel.Message = message;
            this.yesAction = yesAction;
            this.noAction = noAction;
            SetCommands();
        }

        private void SetCommands()
        {
            viewmodel.YesCommand = new RelayCommand<object>(ExecuteYesCommand);
            viewmodel.NoCommand = new RelayCommand<object>(ExecuteNoCommand);
        }

        private void ExecuteYesCommand(object obj)
        {
            yesAction?.Invoke();
            alertDialog.Close();
        }

        private void ExecuteNoCommand(object obj)
        {
            noAction?.Invoke();
            alertDialog.Close();
        }

        public void Show()
        {
            alertDialog.ShowDialog();
        }
    }
}