using CompanyManagement.Views.Dialogs.Interfaces;
using System;

namespace CompanyManagement.Services
{
    public class InputDialogService<TObject>
    {
        private IInputDialog<TObject> inputDialog;

        public InputDialogService(IInputDialog<TObject> inputDialog, 
            TObject obj, Action<TObject> submitObjectCommand)
        {
            this.inputDialog = inputDialog;
            inputDialog.ViewModel.ReceiveObject(obj);
            inputDialog.ViewModel.ReceiveSubmitAction(submitObjectCommand);
        }

        public void Show()
        {
            inputDialog.ShowInputDialog();
        }
    }
}
