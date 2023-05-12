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
        public ICommand AddLeaveCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        public LeaveInputViewModel LeaveInputDataContext { get; private set; }
        private Action<LeaveRequest> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();
        private CheckFormat checker = new CheckFormat();

        public AddLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            AddLeaveCommand = new RelayCommand<Window>(ExecuteAddCommand);
            CloseDialogCommand = new RelayCommand<Window>(CloseCommand);
        }

        private void CloseCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteAddCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            var dialog = new AlertDialogService(
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
            return LeaveInputDataContext.CheckAllFields();
        }

        public void ReceiveObject(LeaveRequest request)
        {
            LeaveInputDataContext.LeaveRequestIns = request;
        }

        public void ReceiveSubmitAction(Action<LeaveRequest> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
