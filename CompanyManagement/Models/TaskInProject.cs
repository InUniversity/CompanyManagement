using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Enums;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {
        private string id;
        private string title;
        private string explanation;
        private DateTime start;
        private DateTime deadline;
        private string progress;
        private string ownerID;
        private string employeeID;
        private string projectID;
        private ETaskStatus status;
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

        public DateTime Start
        {
            get => start;
            set => start = value;
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

        public ETaskStatus Status
        {
            get => status;
            set => status = value;
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

        public TaskInProject(string id, string title, string explanation, DateTime start, 
            DateTime deadline, string progress, string ownerID, string employeeID, 
            string projectID, ETaskStatus status, Employee owner)
        {
            this.id = id;
            this.title = title;
            this.explanation = explanation;
            this.start = start;
            this.deadline = deadline;
            this.progress = progress;
            this.ownerID = ownerID;
            this.employeeID = employeeID;
            this.projectID = projectID;
            this.status = status;
            this.owner = owner;
        }

        public TaskInProject(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.taskID);
                title = Utils.GetString(reader, BaseDao.taskTitle);
                explanation = Utils.GetString(reader, BaseDao.taskExplanation);
                start = Utils.GetDateTime(reader, BaseDao.taskStart);
                deadline = Utils.GetDateTime(reader, BaseDao.taskDeadline);
                progress = Utils.GetString(reader, BaseDao.taskProgress);
                ownerID = Utils.GetString(reader, BaseDao.taskOwnerID);
                employeeID = Utils.GetString(reader, BaseDao.taskEmplID);
                projectID = Utils.GetString(reader, BaseDao.taskProjID);
                status = (ETaskStatus)Utils.GetInt(reader, BaseDao.taskStatusID);
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(TaskInProject), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
