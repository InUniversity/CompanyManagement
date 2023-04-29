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
        private string explanation;
        private DateTime startDate;
        private DateTime deadline;
        private string progress;
        private string ownerID;
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

        public string Explanation
        {
            get => explanation;
            set => explanation = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
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

        public string OwnerID
        {
            get => ownerID;
            set => ownerID = value;
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

        public TaskInProject(string id, string title, string explanation, DateTime startDate, DateTime deadline, 
            string progress, string ownerID, string employeeID, string projectID, string statusID)
        {
            this.id = id;
            this.title = title;
            this.explanation = explanation;
            this.startDate = startDate;
            this.deadline = deadline;
            this.progress = progress;
            this.ownerID = ownerID;
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
                explanation = (string)reader[BaseDao.TASKS_EXPLANATION];
                startDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASKS_START_DATE));
                deadline = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASKS_DEADLINE));
                progress = (string)reader[BaseDao.TASKS_PROGRESS];
                ownerID = (string)reader[BaseDao.TASKS_OWNER_ID];
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
