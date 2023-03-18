using System;
using System.Data;
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
