using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.Dialogs.Interfaces;
using System;
using System.Windows.Input;

namespace CompanyManagement.Services
{
    public class InputDialogService<TObject>
    {
        private IInputDialog<TObject> inputDialog;

        public InputDialogService(IInputDialog<TObject> inputDialog, TObject obj, Action<TObject> submitObjectCommand)
        {
            this.inputDialog = inputDialog;
            IInputViewModel<TObject> inputViewModel = inputDialog.ViewModel;
            inputViewModel.RetrieveObject(obj);
            inputViewModel.RetrieveSubmitAction(submitObjectCommand);
        }

        public void Show()
        {
            inputDialog.ShowInputDialog();
        }
    }
}
