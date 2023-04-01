using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System;
using System.Collections.ObjectModel;
using CompanyManagement.Database.Interfaces;
using System.Collections.Generic;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
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

        private string createBy = SingletonEmployee.Instance.CurrentAccount.EmployeeID;
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

        private string progress = "50";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string employeeID = "";
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private string projectID = "";
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        private string statusID = "1";
        public string StatusID { get => statusID; set { statusID = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        public List<TaskStatus> TaskStatuses { get; set; }

        private ITaskStatusDao taskStatusDao;
        private IProjectAssignmentDao assignmentDao;

        public TaskInputViewModel(IProjectAssignmentDao assignmentDao, ITaskStatusDao taskStatusDao)
        {
            this.taskStatusDao = taskStatusDao;
            this.assignmentDao = assignmentDao;
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            TaskStatuses = taskStatusDao.GetAll();
        }    

        public TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(id, title, description, Utils.DateTimeToString(assignDate), Utils.DateTimeToString(deadline), progress, createBy, employeeID, projectID, statusID);
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

        public void TrimAllTexts()
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
            statusID = taskinproject.Status;
            Employees = new ObservableCollection<Employee>(assignmentDao.GetEmployeesInProject(taskinproject.ProjectID));
        }
    }

    public interface IRetrieveTaskInProject
    {
        void Retrieve(TaskInProject taskinproject);
    }
}
