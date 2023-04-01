using System.Windows.Input;
using System.Windows;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    class UpdateEmployeeViewModel : BaseViewModel
    {
        public ICommand UpdateEmployeeCommand { get; }

        public IEmployees ParentDataContext { get; set; }
        public IEmployeeInput EmployeeInputDataContext { get; }

        public UpdateEmployeeViewModel(IEmployeeInput employeeInputDataContext)
        {
            EmployeeInputDataContext = employeeInputDataContext;
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            if (!CheckAllFields()) return;
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Update(empl);
            inputWindow.Close();
        }

        private bool CheckAllFields()
        {
            return EmployeeInputDataContext.CheckAllFields();
        }
    }
}
