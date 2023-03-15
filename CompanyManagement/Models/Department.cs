using CompanyManagement.Database;
using System.Data;
using System.Windows;
using System;

namespace CompanyManagement.Models
{
    public class Department
    {

        private string id;
        private string name;
        private string managerID;

        public string ID { get { return id; } }

        public string Name { get { return name; } }

        public string ManagerID { get { return managerID;  } }

        public Department() { }

        public Department(string id, string name, string managerId)
        {
            this.id = id;
            this.name = name;
            managerID = managerId;
        }

        public Department(DataRow row)
        {
            try
            {
                id = (string)row[DepartmentDao.ID];
                name = (string)row[DepartmentDao.NAME];
                managerID = (string)row[DepartmentDao.MANAGER_ID];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
