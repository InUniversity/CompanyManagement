﻿using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using System.Windows.Input;
using System.Linq;
using System.Windows.Controls;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ITaskInput
    {
        TaskInProject CreateTask();
        bool CheckAllFields();
        void TrimAllTexts();
        void ReceiveTask(TaskInProject task);
    }

    public class TaskInputViewModel : BaseViewModel, ITaskInput
    {
        private TaskInProject task;
        public string ID { get => task.ID; set { task.ID = value; OnPropertyChanged(); } }
        public string Title { get => task.Title; set { task.Title = value; OnPropertyChanged(); } }
        public string Description { get => task.Description; set { task.Description = value; OnPropertyChanged(); } }
        public DateTime AssignDate { get => task.AssignDate; set { task.AssignDate = value; OnPropertyChanged(); } }
        public DateTime Deadline { get => task.Deadline; set { task.Deadline = value; OnPropertyChanged(); } }
        public string CreateBy { get => task.CreateBy; set { task.CreateBy = value; OnPropertyChanged(); } }
        public string Progress { get => task.Progress; set { task.Progress = value; OnPropertyChanged(); } }
        public string EmployeeID { get => task.EmployeeID; set { task.EmployeeID = value; OnPropertyChanged(); } }
        public string ProjectID { get => task.ProjectID; set { task.ProjectID = value; OnPropertyChanged(); } }
        public string StatusID { get => task.StatusID; set { task.StatusID = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private List<Employee> employees;
        public List<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        private List<TaskStatus> taskStatuses;

        public List<TaskStatus> TaskStatuses { get => taskStatuses; set { taskStatuses = value; OnPropertyChanged(); } }

        private List<Employee> searchedEmployeesCanAssign;
        public List<Employee> SearchedEmployeesCanAssign { get => searchedEmployeesCanAssign; set { searchedEmployeesCanAssign = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand AddEmployeeCommand { get; set; }
        public ICommand GetSelectedEmployeeCommand { get; set; }

        private TaskStatusDao taskStatusDao = new TaskStatusDao();
        private ProjectAssignmentDao assignmentDao = new ProjectAssignmentDao();

        public TaskInputViewModel()
        {
            GetSelectedEmployeeCommand = new RelayCommand<ListView>(ExecuteGetSelectedEmployeeCommand);       
            SetAllComboBox();
        }

        private void ExecuteGetSelectedEmployeeCommand(ListView listView)
        {
            if (listView.SelectedItem == null) return;
            var selectedItem = listView.SelectedItem as Employee;
            EmployeeID = selectedItem.ID;
        }

        private void SetAllComboBox()
        {
            TaskStatuses = taskStatusDao.GetAll();
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(EmployeeID))
            {
                ErrorMessage = "Các thông tin không được để trống!!!";
                return false;
            }
            if (Deadline < AssignDate)
            {
                ErrorMessage = "Thời gian kết thúc không hợp lệ!!!";
                return false;
            }
            return true;
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
            SearchedEmployeesCanAssign = new List<Employee>(searchedItems);
        }

        public TaskInProject CreateTask()
        {
            return task;
        }

        public void TrimAllTexts()
        {
            Title = Title.Trim();
            Description = Description.Trim();
            Progress = Progress.Trim(); 
        }

        public void ReceiveTask(TaskInProject task)
        {
            this.task = task;
            Employees = assignmentDao.GetEmployeesInProject(task.ProjectID);
            SearchedEmployeesCanAssign = new List<Employee>(Employees);
        }
    }
}
