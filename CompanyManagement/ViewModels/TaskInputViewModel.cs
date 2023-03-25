﻿using CompanyManagement.Database.Implementations;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System;
using System.Collections.ObjectModel;
using CompanyManagement.Database.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;

namespace CompanyManagement.ViewModels
{
    public class TaskInputViewModel : BaseViewModel, IRetrieveTaskInProject
    {
        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string title = "";
        public string Title { get => title; set { title = value; OnPropertyChanged(); } }

        private string description = "";
        public string Description { get => description; set { description = value; OnPropertyChanged(); } }

        private DateTime assignDate = DateTime.Now;
        public DateTime AssignDate { get => assignDate; set { assignDate = value; OnPropertyChanged(); } }

        private DateTime deadline = DateTime.Now;
        public DateTime Deadline { get => deadline; set { deadline = value; OnPropertyChanged(); } }

        private string createBy = SingletonEmployee.Instance.CurrentEmployee.ID;
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

        private string progress = "50";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string employeeID = "";
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private string projectID = "";
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        private IProjectAssignmentDao assignmentDao;

        public TaskInputViewModel(IProjectAssignmentDao assignmentDao)
        {
            this.assignmentDao = assignmentDao;
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {

        }

        public TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(ID, Title, Description, Utils.DateTimeToString(AssignDate), Utils.DateTimeToString(Deadline), Progress, CreateBy, EmployeeID, ProjectID);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if(string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(EmployeeID))
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

        public void TrimmAllTexts()
        {
            id = id.Trim();
            title = title.Trim();
            description = description.Trim();
            createBy = createBy.Trim();
            progress = progress.Trim(); 
            employeeID = employeeID.Trim();
            projectID = projectID.Trim();
        }

        public void Retrieve(TaskInProject taskinproject)
        {
            id = taskinproject.ID;
            title = taskinproject.Title;
            description = taskinproject.Description;
            createBy = taskinproject.CreateBy;
            progress = taskinproject.Progress;
            employeeID = taskinproject.EmployeeID;
            projectID = taskinproject.ProjectID;
            Employees = new ObservableCollection<Employee>(assignmentDao.GetEmployeesInProject(taskinproject.ProjectID));
        }
    }
    
    public interface IRetrieveTaskInProject
    {
        void Retrieve(TaskInProject taskinproject);
    }
}
