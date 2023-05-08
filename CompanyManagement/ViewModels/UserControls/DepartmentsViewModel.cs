﻿using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
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
            Departments = departmentsDao.GetAll();
        }

        private void SetCommands()
        {
            OpenAddDialogCommand = new RelayCommand<string>(ExecuteOpenAddDialogCommand);
            DeleteDepartmentCommand = new RelayCommand<string>(Delete);
            OpenUpdateDialogCommand = new RelayCommand<Department>(ExecuteOpenUpdateDialogCommand);
            ItemClickCommand = new RelayCommand<object>(ExecuteItemClickCommand);
        }

        private void ExecuteOpenUpdateDialogCommand(Department dept)
        {
            Log.Instance.Information(nameof(DepartmentsViewModel),"Selected Department: "+dept.Name);
            var inputService = new InputDialogService<Department>(new UpdateDepartmentDialog(), dept, Update);
            inputService.Show();
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
            } while (departmentsDao.SearchByID(deptID) != null);
            return deptID;
        }

        private void ExecuteItemClickCommand(object obj)
        {
            ParentDataContext.MoveToEmployeesInDepartmentView(selectedDepartment);
        }

        private void Delete(string departmentID)
        {
            var dialog = new AlertDialogService(
               "Xóa phòng ban",
               "Bạn chắc chắn muốn Xóa phòng ban !",
               () =>
               {
                   departmentsDao.Delete(departmentID);
                   LoadDepartment();
               }, null);
            dialog.Show();        
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