using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Department
    {
        private string id;
        private string name;
        private string managerID;

        public string ID
        {
            get => id;
            set => id = value;
        } 
            
        public string Name
        {
            get => name;
            set => name = value;
        }

        public string ManagerID
        {
            get => managerID;
            set => managerID = value;
        }

        public Department() { }

        public Department(string id, string name, string managerID)
        {
            this.id = id;
            this.name = name;
            this.managerID = managerID;
        }
        
        public Department(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.DEPARTMENT_ID];
                name = (string)reader[BaseDao.DEPARTMENT_NAME];
                managerID = (string)reader[BaseDao.DEPARTMENT_MANAGER_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Department), "CAST ERROR: " + ex.Message);
            }
        }

        // public override bool Equals(object obj)
        // {
        //     if (obj is not Department department)
        //         throw new Exception("Not the same type");
        //     return base.Equals(obj) && 
        //            string.Equals(department.ID, id) && 
        //            string.Equals(department.Name, name) && 
        //            string.Equals(department.managerID, managerID);
        // }
    }
}
