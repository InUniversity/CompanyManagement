using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {

        private string id;
        private string title;
        private string description;
        private string assignDate;
        private string deadline;
        private string progress;
        private string createBy;
        private string employeeID;
        private string projectID;
        
        public string ID
        {
            get{ return id; }
            set { id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string AssignDate
        {
            get { return assignDate; }
            set { assignDate = value; }
        }

        public string Deadline
        {
            get { return deadline; }
            set { deadline = value; }
        }

        public string Progress
        {
            get { return progress; }
            set { progress = value; }
        }

        public string CreateBy
        {
            get { return createBy; }
            set { createBy = value; }
        }

        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public TaskInProject() { }

        public TaskInProject(string id, string title, string description, string assignDate, string deadline, string progress, string createBy, string employeeID, string projectID)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.assignDate = assignDate;
            this.deadline = deadline;
            this.progress = progress;
            this.createBy = createBy;
            this.employeeID = employeeID;
            this.projectID = projectID;          
        }

        public TaskInProject(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[TaskInProjectDao.TASK_ID];
                title = (string)reader[TaskInProjectDao.TASK_TITLE];
                description = (string)reader[TaskInProjectDao.TASK_DESCRIPTION];
                assignDate = (string)reader[TaskInProjectDao.TASK_ASSIGN_DATE];
                deadline = (string)reader[TaskInProjectDao.TASK_DEADLINE];
                createBy = (string)reader[TaskInProjectDao.TASK_CREATE_BY];
                progress = (string)reader[TaskInProjectDao.TASK_PROGRESS];
                employeeID = (string)reader[TaskInProjectDao.TASK_EMPLOYEE_ID];
                projectID = (string)reader[TaskInProjectDao.TASK_PROJECT_ID];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
