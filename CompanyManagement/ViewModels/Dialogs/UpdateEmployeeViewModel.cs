using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

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
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.UpdateToDB(empl);
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
