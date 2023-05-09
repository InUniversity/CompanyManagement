using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Documents;
using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Department
    {
        private string id;
        private string name;
        private string deptHeadID;
        private Employee deptHead = new Employee();
        private ObservableCollection<Employee> empls = new ObservableCollection<Employee>();

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

        public string DeptHeadID
        {
            get => deptHeadID;
            set => deptHeadID = value;
        }

        public Employee DeptHead
        {
            get => deptHead;
            set => deptHead = value;
        }

        public ObservableCollection<Employee> Empls
        {
            get => empls;
            set => empls = value;
        }

        public Department() { }

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
                id = Utils.GetString(reader, BaseDao.deptID);
                name = Utils.GetString(reader, BaseDao.deptName);
                deptHeadID = Utils.GetString(reader, BaseDao.deptHead);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Department), "CAST ERROR: " + ex.Message);
            }
        }
    }
}