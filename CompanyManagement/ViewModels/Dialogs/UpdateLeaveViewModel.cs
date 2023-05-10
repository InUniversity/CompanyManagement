﻿using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using System;
using CompanyManagement.Database;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateLeaveViewModel: BaseViewModel, IInputViewModel<LeaveRequest>
    {
        public ICommand UpdateLeaveCommand { get; }

        public LeaveInputViewModel LeaveInputDataContext { get; }
        private Action<LeaveRequest> submitObjectAction;

        public UpdateLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            UpdateLeaveCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            var dialog = new AlertDialogService(
                "Cập nhật xin phép nghỉ",
                "Bạn chắc chắn muốn cập nhật xin phép nghỉ !",
                () =>
                {
                    LeaveRequest leaveRequest = LeaveInputDataContext.LeaveRequestIns;
                    submitObjectAction?.Invoke(leaveRequest);
                    inputWindow.Close();
                }, () => { });
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return LeaveInputDataContext.CheckAllFields();
        }

        public void ReceiveObject(LeaveRequest leaveRequest)
        {
            LeaveInputDataContext.LeaveRequestIns = leaveRequest;
            LeaveInputDataContext.RoleName = (new RolesDao()).SearchByID(leaveRequest.Approver.RoleID).Title;
        }

        public void ReceiveSubmitAction(Action<LeaveRequest> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
