using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

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
        
        public ProjectAssignment(IDataRecord reader)
        {
            try
            {
                projectID = (string)reader[BaseDao.PROJECT_ASSIGNMENTS_PROJECT_ID];
                deparmentID = (string)reader[BaseDao.PROJECT_ASSIGNMENTS_DEPARTMENT_ID];
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(ProjectAssignment), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
