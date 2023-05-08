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
        private List<Department> departments;
        public List<Department> Departments { get => departments; set { departments = value; OnPropertyChanged(); } }

        private Department selectedDepartment;
        public Department SelectedDepartment { get => selectedDepartment; set { selectedDepartment = value; OnPropertyChanged(); } }

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
            DeleteDepartmentCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateDialogCommand = new RelayCommand<Department>(ExecuteUpdateCommand);
            ItemClickCommand = new RelayCommand<Department>(ExecuteItemClickCommand);
        }

        private void ExecuteItemClickCommand(Department obj)
        {
            ParentDataContext.MoveToEmployeesInDepartmentView(selectedDepartment);
        }

        private void ExecuteDeleteCommand(string departmentID)
        {
            departmentsDao.Delete(departmentID);
            LoadDepartment();
        }

        private void ExecuteUpdateCommand(Department department)
        {

        }

       
    }
}