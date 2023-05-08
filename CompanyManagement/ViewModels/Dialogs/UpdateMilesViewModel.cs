using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using System;
using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateMilestoneViewModel : BaseViewModel, IInputViewModel<Milestone>
    {
        public ICommand UpdateMilestoneCommand { get; }

        public MilestoneInputViewModel MilestoneInputDataContext { get; }
        private Action<Milestone> submitObjectAction;

        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();

        public UpdateMilestoneViewModel()
        {
            MilestoneInputDataContext = new MilestoneInputViewModel();
            UpdateMilestoneCommand = new RelayCommand<Window>(ExecuteUpdateCommand);
        }

        private void ExecuteUpdateCommand(Window inputWindow)
        {
            MilestoneInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
                "Cập nhật cột mốc",
                "Bạn chắc chắn muốn cập nhật cột mốc cho dự án !",
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
