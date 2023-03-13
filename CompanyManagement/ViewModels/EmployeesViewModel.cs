using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database;
using System.Windows.Input;
using CompanyManagement.Dialogs;

namespace CompanyManagement.ViewModels
{
    public class EmployeesViewModel : BaseViewModel, IRetrieveEmployee
    {

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public ICommand OpenInputDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();

        public EmployeesViewModel()
        {
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            Employees = new ObservableCollection<Employee>();
            DataTable dataTable = employeeDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Employee employee = new Employee(row);
                Employees.Add(employee);
            }
        }

        private void SetCommands()
        {
            OpenInputDialogCommand = new ReplayCommand<BaseViewModel>(OpenEmployeeInputDialog);
            DeleteEmployeeCommand = new ReplayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new ReplayCommand<Employee>(ExecuteUpdateCommand);
        }

        private void OpenEmployeeInputDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            ((EmployeeInputViewModel) addEmployeeDialog.DataContext).ParentViewModel = this;
            addEmployeeDialog.ShowDialog();
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeDao.Delete(id);
            LoadEmployees();
        }

        private void ExecuteUpdateCommand(Employee employee)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            ((EmployeeInputViewModel) addEmployeeDialog.DataContext).ParentViewModel = this;
            addEmployeeDialog.ShowDialog();
        }

        public void Retrieve(Employee employee)
        {
            if (employeeDao.SearchByID(employee.ID) == null)
                employeeDao.Add(employee);
            else
                employeeDao.Save(employee);
            LoadEmployees();
        }
    }

    interface IRetrieveEmployee
    {
        void Retrieve(Employee employee);
    }
}
