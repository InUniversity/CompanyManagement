using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using MaterialDesignThemes.Wpf;

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
            OpenInputDialogCommand = new ReplayCommand<UserControl>(OpenEmployeeInputDialog);
            DeleteEmployeeCommand = new ReplayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new ReplayCommand<Employee>(ExecuteUpdateCommand);
        }

        private void OpenEmployeeInputDialog(UserControl p)
        {
            EmployeeInputWindow employeeInputWindow = new EmployeeInputWindow();
            employeeInputWindow.Owner = Application.Current.MainWindow;
            employeeInputWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            employeeInputWindow.ShowDialog();
        }

        public void Retrieve(Employee employee)
        {
            employeeDao.Add(employee);
            LoadEmployees();
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeDao.Delete(id);
            LoadEmployees();
        }

        private void ExecuteUpdateCommand(Employee employee)
        {
            employeeDao.Save(employee);
            LoadEmployees();
        }
    }

    interface IRetrieveEmployee
    {
        void Retrieve(Employee employee);
    }
}
