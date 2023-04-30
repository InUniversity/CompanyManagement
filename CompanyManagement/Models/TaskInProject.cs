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
        private Employee owner = new Employee();
        private Employee assignedEmployee = new Employee();

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

        public Employee Owner
        {
            get => owner;
            set => owner = value;
        }

        public Employee AssignedEmployee
        {
            get => assignedEmployee;
            set => assignedEmployee = value;
        }

        public TaskInProject() { }

        public TaskInProject(string id, string title, string explanation, DateTime startDate, 
            DateTime deadline, string progress, string ownerID, string employeeID, 
            string projectID, string statusID, Employee owner)
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
            this.owner = owner;
        }

        public TaskInProject(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.TASKS_ID);
                title = Utils.GetString(reader, BaseDao.TASKS_TITLE);
                explanation = Utils.GetString(reader, BaseDao.TASKS_EXPLANATION);
                startDate = Utils.GetDateTime(reader, BaseDao.TASKS_START_DATE);
                deadline = Utils.GetDateTime(reader, BaseDao.TASKS_DEADLINE);
                progress = Utils.GetString(reader, BaseDao.TASKS_PROGRESS);
                ownerID = Utils.GetString(reader, BaseDao.TASKS_OWNER_ID);
                employeeID = Utils.GetString(reader, BaseDao.TASKS_EMPLOYEE_ID);
                projectID = Utils.GetString(reader, BaseDao.TASKS_PROJECT_ID);
                statusID = Utils.GetString(reader, BaseDao.TASKS_STATUS_ID);
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(TaskInProject), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
