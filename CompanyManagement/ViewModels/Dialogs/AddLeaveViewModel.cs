using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddLeaveViewModel : BaseViewModel, IInputViewModel<Leave>
    {
        public ICommand AddLeaveCommand { get; }

        public LeaveInputViewModel LeaveInputDataContext { get; }
        private Action<Leave> submitObjectAction;

        private LeaveDao leaveDao = new LeaveDao();

        public AddLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            AddLeaveCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields())
                return;
            AlertDialogService dialog = new AlertDialogService(
                "Thêm xin nghỉ phép",
                "Bạn chắc chắn muốn thêm xin nghỉ phép !",
                () =>
                {
                    Leave leave = LeaveInputDataContext.CreateLeaveInstance();
                    submitObjectAction?.Invoke(leave);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void ReceiveObject(Leave leave)
        {
            LeaveInputDataContext.Receive(leave);
        }

        public void ReceiveSubmitAction(Action<Leave> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
