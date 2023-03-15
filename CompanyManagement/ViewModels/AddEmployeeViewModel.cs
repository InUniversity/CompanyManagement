using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;

namespace CompanyManagement.ViewModels
{
    public class AddEmployeeViewModel : BaseViewModel
    {

        public ICommand AddEmployeeCommand { get; set; }

        public EmployeesViewModel ParentDataContext { get; set; }
        public EmployeeInputViewModel EmployeeInputDataContext { get; set; }

        private PositionDao positionDao = new PositionDao();
        private DepartmentDao departmentDao = new DepartmentDao();
        
        public AddEmployeeViewModel()
        {
            EmployeeInputDataContext = new EmployeeInputViewModel();
            AddEmployeeCommand = new RelayCommand<Window>(AddCommand, 
                p => EmployeeInputDataContext.CheckAllFields());
        }

        private void AddCommand(Window inputWindow)
        {
            EmployeeInputDataContext.TrimAllTexts();
            Employee empl = EmployeeInputDataContext.CreateEmployeeInstance();
            ParentDataContext.Add(empl);
            EmployeeInputDataContext.ClearAllTexts();
            inputWindow.Close();
        }
    }
}
