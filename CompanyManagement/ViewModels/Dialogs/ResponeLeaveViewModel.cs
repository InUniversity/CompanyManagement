using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.Views.Dialogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public class ResponeLeaveViewModel : BaseViewModel, IInputViewModel<LeaveRequest>
    {
        public ICommand ResponseLeaveCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        public LeaveInputViewModel LeaveInputDataContext { get; private set; }
        private Action<LeaveRequest> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();
        private CheckFormat checker = new CheckFormat();

        public ResponeLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            ResponseLeaveCommand = new RelayCommand<Window>(ExecuteResponseCommand);
            CloseDialogCommand = new RelayCommand<Window>(CloseCommand);
        }

        private void CloseCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteResponseCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            var dialog = new AlertDialogService(
                "Thêm phản hồi",
                "Bạn chắc chắn muốn thêm phản hồi cho nghỉ phép!",
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
