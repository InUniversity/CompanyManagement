using System;
using System.Data.SqlClient;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class ProjectAssignment
    {
        private string projectID;
        private string deparmentID;

        public string ProjectID
        {
            get => projectID;
            set => projectID = value;
        }

        public string DeparmentID
        {
            get => deparmentID;
            set => deparmentID = value;
        }

        public ProjectAssignment(string projectID, string deparmentID)
        {
            this.projectID = projectID;
            this.deparmentID = deparmentID;
        }
        
        public ProjectAssignment(SqlDataReader reader)
        {
            try
            {
                projectID = (string)reader[BaseDao.PROJECT_ASSIGNMENT_PROJECT_ID];
                deparmentID = (string)reader[BaseDao.PROJECT_ASSIGNMENT_DEPARTMENT_ID];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
