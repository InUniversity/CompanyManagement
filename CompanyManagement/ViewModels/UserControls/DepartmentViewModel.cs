﻿using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class DepartmentViewModel : BaseViewModel
    {

        private List<Department> departments;
        public List<Department> Departments { get => departments; set { departments = value; OnPropertyChanged(); } }

        public ICommand DeleteDepartmentCommand { get; set; }
        public ICommand OpenUpdateDialogCommand { get; set; }

        private DepartmentsDao departmentsDao;

        public DepartmentViewModel()
        {
            departmentsDao = new DepartmentsDao();
            LoadDepartment();
            SetCommands();
        }

        private void LoadDepartment()
        {
            departments = departmentsDao.GetAll();
        }

        private void SetCommands()
        {
            DeleteDepartmentCommand = RelayCommand<string>(ExecuteDeleteCommand);
            OpenUpdateDialogCommand = RelayCommand<Department>(ExecuteUpdateCommand);
        }

        private ICommand RelayCommand<T>(Action<T> executeDeleteCommand)
        {
            throw new NotImplementedException();
        }

        private void ExecuteDeleteCommand(string id)
        {
            departmentsDao.Delete(id);
            LoadDepartment();
        }

        private void ExecuteUpdateCommand(Department department)
        {
            // UpdateDepartmentDialog updateDepartmentDialog = new UpdateDepartmentDialog();
            // IDialogViewModel updateDepartmentModel = (IDialogViewModel)updateDepartmentDialog.DataContext;
            // updateDepartmentModel.ParentDataContext = this;
            // updateDepartmentModel.Retrieve(department);
            // updateDepartmentDialog.ShowDialog();
        }

        public void UpdateToDB(object department)
        {
            departmentsDao.Update(department as Department);
            LoadDepartment();
        }

        public void AddToDB(object obj)
        {
            throw new NotImplementedException();
        }
    }
}