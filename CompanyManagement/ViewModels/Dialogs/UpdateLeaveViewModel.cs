using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateLeaveViewModel: BaseViewModel, IDialogViewModel
    {
        
        public ICommand UpdateLeaveCommand { get; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public ILeaveInput LeaveInputDataContext { get; }

        public UpdateLeaveViewModel()
        {
            LeaveInputDataContext = new LeaveInputViewModel();
            UpdateLeaveCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            LeaveInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
               "Cập nhật xin phép nghỉ",
               "Bạn chắc chắn muốn cập nhật xin phép nghỉ !",
               () =>
               {
                   Leave empl = LeaveInputDataContext.CreateLeaveInstance();
                   ParentDataContext.UpdateToDB(empl);
                   inputWindow.Close();
               }, () => { });
            dialog.Show();
        }

        private bool CheckAllFields()
        {
            return true;
        }

        public void Retrieve(object leave)
        {
            LeaveInputDataContext.Retrieve(leave as Leave);
        }
    }
}
