﻿using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class EmployeesInDepartmentViewModel : BaseViewModel
    {
        private List<Employee> employees;
        public List<Employee> Employees { get => employees; set => employees = value; }

        private Department department;
        public Department Department { get => department; set { department = value; OnPropertyChanged(); } }

        private List<Employee> searchedEmployees;
        public List<Employee> SearchedEmployees { get => searchedEmployees; set { searchedEmployees = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand BackDepartmentViewCommand { get; private set; }
        public ICommand OpenAddDialogCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }
        public ICommand OpenUpdateDialogCommand { get; private set; }

        private EmployeesDao employeesDao = new EmployeesDao();
        private AccountsDao accountsDao = new AccountsDao();
        private RolesDao rolesDao = new RolesDao();

        public IOrganization ParentDataContext { get; set; }

        public EmployeesInDepartmentViewModel()
        {
            SetCommands();
        }

        private void ExecuteBackDepartmentViewCommand(object obj)
        {
            ParentDataContext.MoveToDepartmentsView();
        }

        public void LoadEmployees()
        {
            Employees = employeesDao.SearchByDepartmentID(department.ID);
            GetRoleForListEmployees(Employees);
            SearchedEmployees = Employees;
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<object>(OpenAddEmployeeDialog);
            DeleteEmployeeCommand = new RelayCommand<string>(DeleteEmployee);
            OpenUpdateDialogCommand = new RelayCommand<Employee>(OpenUpdateEmployeeDialog);
            BackDepartmentViewCommand = new RelayCommand<object>(ExecuteBackDepartmentViewCommand);
        }

        private void GetRoleForListEmployees(List<Employee> employees)
        {
            foreach (Employee empl in employees)
                empl.EmplRole = rolesDao.SearchByID(empl.RoleID);
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

        private void OpenAddEmployeeDialog(object obj)
        {
            var employee = CreateEmployee();
            employee.DepartmentID = Department.ID;
            var inputService = new InputDialogService<Employee>(new AddEmployeeDialog(), employee, Add);
            inputService.Show();
        }

        private Employee CreateEmployee()
        {
            return new Employee(AutoGenerateID(), "", "", DateTime.Now,
                "", "", "", "", "","");
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
            AlertDialogService dialog = new AlertDialogService(
              "Xóa nhân viên",
              "Bạn chắc chắn muốn xóa nhân viên !",
              () =>
              {
                  employeesDao.Delete(id);
                  accountsDao.Delete(id);
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
            employeesDao.Update(employee);
            LoadEmployees();
        }
    }
}
