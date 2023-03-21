using System;
using System.Data.SqlClient;
using System.Windows;
using CompanyManagement.Database;

namespace CompanyManagement.Models
{
    public class Department
    {

        private string id;
        private string name;
        private string managerID;

        public string ID
        {
            get { return id; }
            set { id = value; }
        } 
            
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
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
                MessageBox.Show(ex.Message);
            }
        }
    }
}
