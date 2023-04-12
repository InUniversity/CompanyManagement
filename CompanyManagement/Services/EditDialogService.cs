using System;
using System.Windows;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.Services
{
    public class EditDialogService<TViewModel, TResult>
    {
        private Window dialog = new Window();
        private BaseViewModel dialogViewModel;
        private TViewModel contentViewModel;
        
        private Action<TResult> editDataAction;

        public EditDialogService(UserControl dialogContent, Action<TResult> editDataAction)
        {
            // dialogViewModel.CurrentChildView = dialogContent;
            contentViewModel = (TViewModel)dialogContent.DataContext;
            this.editDataAction = editDataAction;
        }

        public void Show()
        {
            dialog.ShowDialog();
        }
    }
}
