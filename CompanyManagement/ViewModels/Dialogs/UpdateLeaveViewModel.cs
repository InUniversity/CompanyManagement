using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using System;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateLeaveViewModel: BaseViewModel, IInputViewModel<Leave>
    {
        public ICommand UpdateLeaveCommand { get; }

        public ILeaveInput LeaveInputDataContext { get; }
        private Action<Leave> submitObjectAction;

        public UpdateLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            UpdateLeaveCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
                "Cập nhật xin phép nghỉ",
                "Bạn chắc chắn muốn cập nhật xin phép nghỉ !",
                () =>
                {
                    Leave empl = LeaveInputDataContext.CreateLeaveInstance();
                    inputWindow.Close();
                }, () => { });
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void ReceiveObject(Leave leave)
        {
            LeaveInputDataContext.Retrieve(leave);
        }

        public void ReceiveSubmitAction(Action<Leave> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
