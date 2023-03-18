using System.Windows.Input;
using System.Windows;

namespace CompanyManagement.ViewModels
{
    class UpdateEmployeeViewModel : BaseViewModel
    {

        public ICommand UpdateEmployeeCommand { get; set; }

        public EmployeesViewModel ParentDataContext { get; set; }
        public EmployeeInputViewModel EmployeeInputDataContext { get; set; }

        public UpdateEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            UpdateEmployeeCommand = new RelayCommand<Window>(UpdateCommand, p => CheckAllFields());
        }

        private void UpdateCommand(Window inputWindow)
        {
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
