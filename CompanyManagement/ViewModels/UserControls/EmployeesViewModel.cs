using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.UserControls
{
    public class EmployeesViewModel : BaseViewModel, IEditDBViewModel
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

        private void OpenAddEmployeeDialog(object obj)
        {
            AddEmployeeDialog addEmployeeDialog = new AddEmployeeDialog();
            IDialogViewModel addEmployeeVM = (IDialogViewModel)addEmployeeDialog.DataContext;
            addEmployeeVM.ParentDataContext = this;
            Employee employee = CreateEmployee();
            addEmployeeVM.Retrieve(employee);
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
            IDialogViewModel updateEmployeeVM = (IDialogViewModel)updateEmployeeDialog.DataContext;
            updateEmployeeVM.ParentDataContext = this;
            updateEmployeeVM.Retrieve(employee);
            updateEmployeeDialog.ShowDialog();
        }

        public void AddToDB(object employee)
        {
            employeeAccountDao.Add(employee as Employee);
            LoadEmployees();
        }

        public void UpdateToDB(object employee)
        {
            employeeAccountDao.Update(employee as Employee);
            LoadEmployees();
        }
    }
}