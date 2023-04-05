using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IEmployees
    {
        void Add(Employee employee);
        void Update(Employee employee);
    }

    public class EmployeesViewModel : BaseViewModel, IEmployees
    {
        
        private List<Employee> employees;

        private List<Employee> searchedEmployees;
        public List<Employee> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand OpenAddDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private EmployeeDao employeeAccountDao;
        private AccountDao accountDao;

        public EmployeesViewModel()
        {
            employeeAccountDao = new EmployeeDao();
            accountDao = new AccountDao();
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            employees = employeeAccountDao.GetAll();
            SearchedEmployees = employees;
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(DeleteEmployee);
            OpenUpdateDialogCommand = new RelayCommand<Employee>(OpenUpdateEmployeeDialog);
        }

        private void SearchByName()
        {
            var searchedItems = employees;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = employees
                    .Where(item => item.Name.Contains(textToSearch, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            SearchedEmployees = searchedItems;
        }

        private void OpenAddEmployeeDialog(object p)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            IAddEmployee addEmployeeVM = (IAddEmployee)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            Employee employee = CreateEmployee();
            addEmployeeVM.EmployeeInputDataContext.Retrieve(employee);
            addEmployeeDialog.ShowDialog();
        }

        private Employee CreateEmployee()
        {
            return new Employee(AutoGenerateID(), "", "", DateTime.Now,
                "", "", "", "", "", "", 0);
        }

        private string AutoGenerateID()
        {
            string employeeID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                employeeID = $"EM{number:0000}";
            } while (employeeAccountDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void DeleteEmployee(string id)
        {
            employeeAccountDao.Delete(id);
            accountDao.Delete(id);
            LoadEmployees();
        }

        private void OpenUpdateEmployeeDialog(Employee employee)
        {
            UpdateEmployeeDialog updateEmployeeDialog = new UpdateEmployeeDialog();
            IUpdateEmployee updateEmployeeVM = (IUpdateEmployee)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.EmployeeInputDataContext.Retrieve(employee);
            updateEmployeeDialog.ShowDialog();
        }

        public void Add(Employee employee)
        {
            employeeAccountDao.Add(employee);
            LoadEmployees();
        }

        public void Update(Employee employee)
        {
            employeeAccountDao.Update(employee);
            LoadEmployees();
        }
    }
}