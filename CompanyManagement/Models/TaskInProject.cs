using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {
        private string id;
        private string title;
        private string description;
        private DateTime assignDate;
        private DateTime deadline;
        private string progress;
        private string createBy;
        private string employeeID;
        private string projectID;
        private string statusID;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public DateTime AssignDate
        {
            get => assignDate;
            set => assignDate = value;
        }

        public DateTime Deadline
        {
            get => deadline;
            set => deadline = value;
        }

        public string Progress
        {
            get => progress;
            set => progress = value;
        }

        public string CreateBy
        {
            get => createBy;
            set => createBy = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public string ProjectID
        {
            get => projectID;
            set => projectID = value;
        }

        public string StatusID
        {
            get => statusID;
            set => statusID = value;
        }

        public TaskInProject() { }

        public TaskInProject(string id, string title, string description, DateTime assignDate, DateTime deadline, 
            string progress, string createBy, string employeeID, string projectID, string statusID)
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
            this.statusID = statusID;
        }

        public TaskInProject(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.TASKS_ID];
                title = (string)reader[BaseDao.TASKS_TITLE];
                description = (string)reader[BaseDao.TASKS_DESCRIPTION];
                assignDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASKS_ASSIGN_DATE));
                deadline = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASKS_DEADLINE));
                createBy = (string)reader[BaseDao.TASKS_CREATE_BY];
                progress = (string)reader[BaseDao.TASKS_PROGRESS];
                employeeID = (string)reader[BaseDao.TASKS_EMPLOYEE_ID];
                projectID = (string)reader[BaseDao.TASKS_PROJECT_ID];
                statusID = (string)reader[BaseDao.TASKS_STATUS_ID];
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(TaskInProject), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
