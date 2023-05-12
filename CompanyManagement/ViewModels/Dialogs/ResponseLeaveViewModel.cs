using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using System;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs.Interfaces
{
    public class ResponseLeaveViewModel : BaseViewModel, IInputViewModel<LeaveRequest>
    {
        private LeaveRequest request;
        public string Response { get => request.Response; set { request.Response = value; OnPropertyChanged(); } }

        public ICommand ResponseLeaveCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        private Action<LeaveRequest> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();
        private CheckFormat checker = new CheckFormat();

        public ResponseLeaveViewModel()
        {
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
            TrimAllText();
            var dialog = new AlertDialogService(
                "Thêm phản hồi",
                "Bạn chắc chắn muốn thêm phản hồi cho nghỉ phép!",
                () =>
                {
                    var leavRequest = request;
                    submitObjectAction?.Invoke(leavRequest);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }

        private void TrimAllText()
        {
            Response = Response.Trim();
        }

        public void ReceiveObject(LeaveRequest request)
        {
            this.request = request;
        }

        public void ReceiveSubmitAction(Action<LeaveRequest> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
