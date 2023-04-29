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
    public class AddLeaveViewModel : BaseViewModel, IInputViewModel<LeaveRequest>
    {
        public ICommand AddLeaveCommand { get; }

        public LeaveInputViewModel LeaveInputDataContext { get; }
        private Action<LeaveRequest> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();

        public AddLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            AddLeaveCommand = new RelayCommand<Window>(ExecuteAddCommand);
        }

        private void ExecuteAddCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
                "Thêm xin nghỉ phép",
                "Bạn chắc chắn muốn thêm xin nghỉ phép !",
                () =>
                {
                    LeaveRequest leaveRequest = LeaveInputDataContext.LeaveRequestIns;
                    submitObjectAction?.Invoke(leaveRequest);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void ReceiveObject(LeaveRequest leaveRequest)
        {
            LeaveInputDataContext.LeaveRequestIns = leaveRequest;
        }

        public void ReceiveSubmitAction(Action<LeaveRequest> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
