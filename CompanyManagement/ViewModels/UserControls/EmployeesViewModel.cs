using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Services;
using CompanyManagement.Models;

namespace CompanyManagement.ViewModels.UserControls
{
    public class EmployeesViewModel : BaseViewModel
    {
        private List<Employee> employees;

        private List<Employee> searchedEmployees;
        public List<Employee> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand OpenAddDialogCommand { get; set; }
        public ICommand DeleteEmployeeCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private EmployeeDao employeeDao = new EmployeeDao();
        private AccountDao accountDao = new AccountDao();

        public EmployeesViewModel()
        {
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            employees = employeeDao.SearchByCurrentID(CurrentUser.Instance.CurrentEmployee.ID);
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
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedEmployees = searchedItems;
        }

        private void OpenAddEmployeeDialog(object obj)
        {
            var employee = CreateEmployee();
            var inputService = new InputDialogService<Employee>(new AddEmployeeDialog(), employee, Add);
            inputService.Show();
        }

        private Employee CreateEmployee()
        {
            return new Employee(AutoGenerateID(), "", "", DateTime.Now,
                "", "", "", "", "", "", 0);
        }

        private void Add(Employee employee)
        {
            employeeDao.Add(employee);
            LoadEmployees();
        }

        private string AutoGenerateID()
        {
            string employeeID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                employeeID = $"EM{number:0000}";
            } while (employeeDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void DeleteEmployee(string id)
        {
            AlertDialogService dialog = new AlertDialogService(
              "Xóa nhân viên",
              "Bạn chắc chắn muốn xóa nhân viên !",
              () =>
              {
                  employeeDao.Delete(id);
                  accountDao.Delete(id); 
                  LoadEmployees();
              }, () => { });
            dialog.Show();
        }

        private void OpenUpdateEmployeeDialog(Employee employee)
        {
            var inputService = new InputDialogService<Employee>(new UpdateEmployeeDialog(), employee, Update);
            inputService.Show();
        }

        private void Update(Employee employee)
        {
            employeeDao.Update(employee);
            LoadEmployees();
        }
    }
}