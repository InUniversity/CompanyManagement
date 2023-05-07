using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class DepartmentsViewModel : BaseViewModel
    {
        private List<Department> departments;
        public List<Department> Departments { get => departments; set { departments = value; OnPropertyChanged(); } }

        private Department selectedDepartment;
        public Department SelectedDepartment { get => selectedDepartment; set { selectedDepartment = value; OnPropertyChanged(); } }

        public ICommand OpenAddDialogCommand { get; private set; }
        public ICommand DeleteDepartmentCommand { get; private set; }
        public ICommand OpenUpdateDialogCommand { get; private set; }
        public ICommand ItemClickCommand { get; private set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();

        public IOrganization ParentDataContext { get;set; }

        public DepartmentsViewModel()
        {
            LoadDepartment();
            SetCommands();
        }

        private void LoadDepartment()
        {
            departments = departmentsDao.GetAll();
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<string>(ExecuteOpenAddDialogCommand);
            DeleteDepartmentCommand = new RelayCommand<string>(Delete);
            OpenUpdateDialogCommand = new RelayCommand<Department>(ExecuteOpenUpdateDialogCommand);
            ItemClickCommand = new RelayCommand<object>(ExecuteItemClickCommand);
        }

        private void ExecuteOpenUpdateDialogCommand(Department obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteOpenAddDialogCommand(string obj)
        {
            var dept = CreateDepartment();
            var inputService = new InputDialogService<Department>(new AddDepartmentDialog(), dept, Add);
            inputService.Show();
        }

        private Department CreateDepartment()
        {
            return new Department(AutoGenerateID(), "", "");
        }

        private string AutoGenerateID()
        {
            string deptID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                deptID = $"EM{number:0000}";
            } while (departmentsDao.DepartmentByEmployeeDeptID(deptID) != null);
            return deptID;
        }

        private void ExecuteItemClickCommand(object obj)
        {
            ParentDataContext.MoveToEmployeesInDepartmentView(selectedDepartment);
        }

        private void Delete(string departmentID)
        {
            departmentsDao.Delete(departmentID);
            LoadDepartment();
        }

        private void Add(Department department)
        {
            departmentsDao.Add(department);
            LoadDepartment();
        }

        private void Update(Department department)
        {
            departmentsDao.Update(department);
            LoadDepartment();
        }

    }
}