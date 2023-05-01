using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class ProjectAssignment
    {
        private string projID;
        private string deptID;

        public string ProjID
        {
            get => projID;
            set => projID = value;
        }

        public string DeparmentID
        {
            get => deptID;
            set => deptID = value;
        }

        public ProjectAssignment(string projID, string deptID)
        {
            this.projID = projID;
            this.deptID = deptID;
        }
        
        public ProjectAssignment(IDataRecord reader)
        {
            try
            {
                projID = (string)reader[BaseDao.projAssignID];
                deptID = (string)reader[BaseDao.projAssignDeptID];
            }
            catch(Exception ex)
            {
                Log.Instance.Error(nameof(ProjectAssignment), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
