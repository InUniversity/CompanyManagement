using System;
using System.Data;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class TaskInProject
    {
        private string id;
        private string tile;
        private string description;
        private string assignDate;
        private string deadline;
        private string progress;
        private string createBy;
        private string employeeID;
        private string projectID;
        
        public string ID
        {
            get{ return this.id; }
            set { this.id = value; }
        }

        public string Tile
        {
            get { return this.tile; }
            set { this.tile = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }
        public string AssignDate
        {
            get { return this.assignDate; }
            set { this.assignDate = value; }
        }

        public string Deadline
        {
            get { return this.deadline; }
            set { this.deadline = value; }
        }
        public string Progress
        {
            get { return this.progress; }
            set { this.progress = value; }
        }
        public string CreateBy
        {
            get { return this.createBy; }
            set { this.createBy = value; }
        }
        public string EmployeeID
        {
            get { return this.employeeID; }
            set { this.employeeID = value; }
        }
        public string ProjectID
        {
            get { return this.projectID; }
            set { this.projectID = value; }
        }

        public TaskInProject() { }

        public TaskInProject(string id, string tile, string description, string assignDate, string deadline, string progress, string createBy, string employeeID, string projectID)
        {
            this.id = id;
            this.tile = tile;
            this.description = description;
            this.assignDate = assignDate;
            this.deadline = deadline;
            this.progress = progress;
            this.createBy = createBy;
            this.employeeID = employeeID;
            this.projectID = projectID;          
        }

        public TaskInProject(DataRow row)
        {
            try
            {
                this.id = (string)row[TaskInProjectDao.ID];
                this.tile = (string)row[TaskInProjectDao.TILE];
                this.description = (string)row[TaskInProjectDao.DESCRIPTION];
                this.assignDate = (string)row[TaskInProjectDao.ASSIGN_DATE];
                this.deadline = (string)row[TaskInProjectDao.DEADLINE];
                this.createBy = (string)row[TaskInProjectDao.CREATEBY];
                this.progress = (string)row[TaskInProjectDao.PROGRESS];
                this.employeeID = (string)row[TaskInProjectDao.EMPLOYEE_ID];
                this.projectID = (string)row[TaskInProjectDao.PROJECT_ID];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
