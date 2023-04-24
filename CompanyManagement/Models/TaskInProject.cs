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
        private string status;

        public string ID => id;
        public string Title => title;
        public string Description => description;
        public DateTime AssignDate => assignDate;
        public DateTime Deadline => deadline;
        public string Progress => progress;
        public string CreateBy => createBy;
        public string EmployeeID => employeeID;
        public string ProjectID => projectID;
        public string Status => status;

        public TaskInProject() { }

        public TaskInProject(string id, string title, string description, DateTime assignDate, DateTime deadline, 
            string progress, string createBy, string employeeID, string projectID, string status)
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
            this.status = status;
        }

        public TaskInProject(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.TASK_ID];
                title = (string)reader[BaseDao.TASK_TITLE];
                description = (string)reader[BaseDao.TASK_DESCRIPTION];
                assignDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASK_ASSIGN_DATE));
                deadline = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASK_DEADLINE));
                createBy = (string)reader[BaseDao.TASK_CREATE_BY];
                progress = (string)reader[BaseDao.TASK_PROGRESS];
                employeeID = (string)reader[BaseDao.TASK_EMPLOYEE_ID];
                projectID = (string)reader[BaseDao.TASK_PROJECT_ID];
                status = (string)reader[BaseDao.TASK_STATUS_ID];
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(TaskInProject), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
