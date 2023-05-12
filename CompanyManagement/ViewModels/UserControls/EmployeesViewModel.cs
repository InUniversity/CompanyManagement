using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Services;
using CompanyManagement.Enums;

namespace CompanyManagement.ViewModels.UserControls
{
    public class EmployeesViewModel : BaseViewModel
    {
        private List<Employee> employees;
        public List<Employee> Employees { get => employees; set => employees = value; }

        private List<Employee> searchedEmployees;
        public List<Employee> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand OpenAddDialogCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }
        public ICommand OpenUpdateDialogCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();
        private AccountsDao accountsDao = new AccountsDao();
        private RolesDao rolesDao = new RolesDao();

        public EmployeesViewModel()
        {
            LoadEmployees();
            SetCommands();
        }

        private void LoadEmployees()
        {
            var listAllEmpl = employeesDao.GetAllWithoutManagers();
            var listItem = from empl in listAllEmpl 
                           where empl.DepartmentID == "" && empl.EmplRole.Perms != EPermission.HR
                           select empl;
            GetRoleForListEmployees(listItem.ToList());
            employees = listItem.ToList();
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
            var searchedItems = Employees;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = Employees
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedEmployees = searchedItems;
        }

        private void GetRoleForListEmployees(List<Employee> employees)
        {
            foreach (var empl in employees)
                empl.EmplRole = rolesDao.SearchByID(empl.RoleID);
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
                "", "", "", "", "", "");
        }

        private void Add(Employee employee)
        {
            employeesDao.Add(employee);
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
            } while (employeesDao.SearchByID(employeeID) != null);
            return employeeID;
        }

        private void DeleteEmployee(string id)
        {
            var dialog = new AlertDialogService(
              "Xóa nhân viên",
              "Bạn chắc chắn muốn xóa nhân viên !",
              () =>
              {
                  employeesDao.Delete(id);
                  accountsDao.Delete(id); 
                  LoadEmployees();
              }, null);
            dialog.Show();
        }

        private void OpenUpdateEmployeeDialog(Employee employee)
        {
            var inputService = new InputDialogService<Employee>(new UpdateEmployeeDialog(), employee, Update);
            inputService.Show();
        }

        private void Update(Employee employee)
        {
            employeesDao.Update(employee);
            LoadEmployees();
        }
    }
}