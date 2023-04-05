using CompanyManagement.Models;
using System;
using System.Collections.Generic;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ITaskInput
    {
        TaskInProject CreateTaskInProjectInstance();
        bool CheckAllFields();
        void TrimAllTexts();
        void RetrieveTask(TaskInProject taskInProject);
    }

    public class TaskInputViewModel : BaseViewModel, ITaskInput 
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

        private string progress = "";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string employeeID = "";
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private string projectID = "";
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        private string statusID = "";
        public string StatusID { get => statusID; set { statusID = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private List<Employee> employees;
        public List<Employee> Employees { get => employees; set { employees = value; OnPropertyChanged(); } }

        private List<TaskStatus> taskStatuses;
        public List<TaskStatus> TaskStatuses { get => taskStatuses; set { taskStatuses = value; OnPropertyChanged(); } }

        private TaskStatusDao taskStatusDao;
        private ProjectAssignmentDao assignmentDao;

        public TaskInputViewModel()
        {
            taskStatusDao = new TaskStatusDao();
            assignmentDao = new ProjectAssignmentDao();
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            TaskStatuses = taskStatusDao.GetAll();
        }

        public TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(id, title, description, assignDate, deadline, progress, createBy, employeeID, projectID, statusID);
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
            ID = id.Trim();
            Title = title.Trim();
            Description = description.Trim();
            CreateBy = createBy.Trim();
            Progress = progress.Trim(); 
            EmployeeID = employeeID.Trim();
            ProjectID = projectID.Trim();
        }

        public void RetrieveTask(TaskInProject taskInProject)
        {
            ID = taskInProject.ID;
            Title = taskInProject.Title;
            Description = taskInProject.Description;
            AssignDate = taskInProject.AssignDate;
            Deadline = taskInProject.Deadline;
            CreateBy = taskInProject.CreateBy;
            Progress = taskInProject.Progress;
            EmployeeID = taskInProject.EmployeeID;
            ProjectID = taskInProject.ProjectID;
            StatusID = taskInProject.Status;
            Employees = assignmentDao.GetEmployeesInProject(taskInProject.ProjectID);
        }
    }
}
