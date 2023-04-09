using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class UpdateEmployeeViewModel : BaseViewModel, IDialogViewModel
    {
        
        public ICommand UpdateEmployeeCommand { get; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; }

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            if (!CheckAllFields()) return;
            AlertDialogService dialog = new AlertDialogService(
               "Cập nhật nhân viên",
               "Bạn chắc chắn muốn cập nhật nhân viên !",
               () =>
               {
                   Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
                   ParentDataContext.UpdateToDB(empl);
               }, () => { });
            dialog.Show();              
            inputWindow.Close();
        }

        private bool CheckAllFields()
        {
            return EmployeeInputDataContext.CheckAllFields();
        }
        
        public void Retrieve(object employee)
        {
            EmployeeInputDataContext.Retrieve(employee as Employee);
        }
    }
}
