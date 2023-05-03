using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class DepartmentsViewModel : BaseViewModel
    {
        private List<Department> departments = new List<Department>();
        public List<Department> Departments { get => departments; set { departments = value; OnPropertyChanged(); } }

        public ICommand DeleteDepartmentCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private DepartmentsDao departmentsDao = new DepartmentsDao();

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
            DeleteDepartmentCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateDialogCommand = new RelayCommand<Department>(ExecuteUpdateCommand);
        }

        private void ExecuteDeleteCommand(string id)
        {
            departmentsDao.Delete(id);
            LoadDepartment();
        }

        private void ExecuteUpdateCommand(Department department)
        {
        }
    }
}