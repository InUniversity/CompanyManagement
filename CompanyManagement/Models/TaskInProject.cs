﻿using System;
using System.Data.SqlClient;
using CompanyManagement.Database;
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

        public DateTime AssignDate
        {
            get { return assignDate; }
            set { assignDate = value; }
        }

        public DateTime Deadline
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

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

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

        public TaskInProject(SqlDataReader reader)
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
