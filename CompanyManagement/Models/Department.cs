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
        private string departmentHeadID;

        public string ID => id;
        public string Name => name;
        public string DepartmentHeadID => departmentHeadID;

        public Department(string id, string name, string departmentHeadID)
        {
            this.id = id;
            this.name = name;
            this.departmentHeadID = departmentHeadID;
        }
        
        public Department(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.DEPARTMENTS_ID];
                name = (string)reader[BaseDao.DEPARTMENTS_NAME];
                departmentHeadID = (string)reader[BaseDao.DEPARTMENTS_HEAD];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Department), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
