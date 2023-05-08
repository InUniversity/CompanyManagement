﻿using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls;
using System;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddMilestoneViewModel: BaseViewModel, IInputViewModel<Milestone>
    {
        public ICommand AddMilestoneCommand { get; }

        public MilestoneInputViewModel MilestoneInputDataContext { get; }
        private Action<Milestone> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();

        public AddMilestoneViewModel()
        {
            MilestoneInputDataContext = new MilestoneInputViewModel();
            AddMilestoneCommand = new RelayCommand<Window>(ExecuteAddCommand);
        }

        private void ExecuteAddCommand(Window inputWindow)
        {
            MilestoneInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
                "Thêm cột mốc",
                "Bạn chắc chắn muốn thêm cột mốc cho dự án !",
                () =>
                {
                    Milestone milestone = MilestoneInputDataContext.MilestoneIns;
                    submitObjectAction?.Invoke(milestone);
                    inputWindow.Close();
                }, null);
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void ReceiveObject(Milestone milestone)
        {
            MilestoneInputDataContext.MilestoneIns = milestone;
        }

        public void ReceiveSubmitAction(Action<Milestone> submitObjectAction)
        {
            
            this.submitObjectAction = submitObjectAction;
        }
    }
}
