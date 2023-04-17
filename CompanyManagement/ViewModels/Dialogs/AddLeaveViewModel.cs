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
        public ICommand AddLeaveCommand { get; set; }

        public LeaveInputViewModel LeaveInputDataContext;
        private Action<Leave> submitObjectAction;

        private LeaveDao leaveDao;

        public AddLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            leaveDao = new LeaveDao();
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
                }, () => { });
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void RetrieveObject(Leave leave)
        {
            LeaveInputDataContext.Retrieve(leave);
        }

        public void RetrieveSubmitAction(Action<Leave> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
