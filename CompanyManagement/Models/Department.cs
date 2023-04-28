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
        private string managerID;

        public string ID => id;
        public string Name => name;
        public string ManagerID => managerID;

        public Department(string id, string name, string managerID)
        {
            this.id = id;
            this.name = name;
            this.managerID = managerID;
        }
        
        public Department(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.DEPARTMENTS_ID];
                name = (string)reader[BaseDao.DEPARTMENTS_NAME];
                managerID = (string)reader[BaseDao.DEPARTMENTS_MANAGER_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Department), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
