using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Department
    {
        private string id;
        private string name;
        private string deptHeadID;

        public string ID => id;
        public string Name => name;
        public string DeptHeadID => deptHeadID;

        public Department(string id, string name, string deptHeadID)
        {
            this.id = id;
            this.name = name;
            this.deptHeadID = deptHeadID;
        }
        
        public Department(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.deptID];
                name = (string)reader[BaseDao.deptName];
                deptHeadID = (string)reader[BaseDao.deptHead];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Department), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
