using CompanyManagement.Database;
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
        public ICommand CloseDialogCommand { get; }

        public MilestoneInputViewModel MilestoneInputDataContext { get; }
        private Action<Milestone> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();

        public AddMilestoneViewModel()
        {
            MilestoneInputDataContext = new MilestoneInputViewModel();
            AddMilestoneCommand = new RelayCommand<Window>(AddCommand);
            CloseDialogCommand = new RelayCommand<Window>(CloseCommand);
        }

        private void CloseCommand(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
            "Thêm cột mốc",
            "Bạn chắc chắn muốn thoát !",
            () =>
            {
                window.Close();
            }, null);
            dialog.Show();
        }

        private void AddCommand(Window inputWindow)
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
