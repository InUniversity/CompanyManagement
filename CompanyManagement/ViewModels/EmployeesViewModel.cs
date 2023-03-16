using System.Collections.ObjectModel;
using System.Data;
using CompanyManagement.Database;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels
{
    public class EmployeesViewModel : BaseViewModel, IEmployees
    {

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts { get => accounts; set { accounts = value; OnPropertyChanged(); } }

        public ICommand OpenInputDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand UpdateEmployeeCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();
        private AccountDao accountDao = new AccountDao();

        public EmployeesViewModel()
        {
            LoadEmployees();
            LoadAccounts();
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

        private void LoadAccounts()
        {
            Accounts = new ObservableCollection<Account>();
            DataTable dataTable = accountDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Account account = new Account(row);
                Accounts.Add(account);
            }
        }

        private void SetCommands()
        {
            OpenInputDialogCommand = new RelayCommand<object>(OpenEmployeeInputDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateEmployeeCommand = new RelayCommand<Employee>(ExecuteUpdateCommand);
        }

        private void OpenEmployeeInputDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            AddEmployeeViewModel addEmployeeVM = (AddEmployeeViewModel)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            addEmployeeDialog.ShowDialog();
        }

        private void ExecuteDeleteCommand(string id)
        {
            employeeDao.Delete(id);
            LoadEmployees();
        }

        private void ExecuteUpdateCommand(Employee employee)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog();
            UpdateEmployeeViewModel updateEmployeeVM = (UpdateEmployeeViewModel)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.EmployeeInputDataContext.Retrieve(employee);
            updateEmployeeDialog.ShowDialog();
        }

        public void Add(Employee employee)
        {
            employeeDao.Add(employee);
            LoadEmployees();
        }

        public void Update(Employee employee)
        {
            employeeDao.Save(employee);
            
            LoadEmployees();
        }
    }

    public interface IEmployees
    {
        void Add(Employee employee);
        void Update(Employee employee);                                
    }
}
