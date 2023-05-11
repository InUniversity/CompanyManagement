using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Role
    {
        private string id;
        private Permission perms;
        private string title;
        private decimal salary;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public Permission Perms
        {
            get => perms;
            set => perms = value;
        }

        public string Title
        {
            get => title;
            set => title = value;
        }

        public decimal Salary
        {
            get => salary;
            set => salary = value;
        }

        public Role() { }
        
        public Role(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.roleID);
                string permsStr = Utils.GetString(record, BaseDao.rolePerms);
                perms = Enum.TryParse(permsStr, out Permission per) ? per : Permission.NotAllow;
                title = Utils.GetString(record, BaseDao.roleTitle);
                salary = Utils.GetDecimal(record, BaseDao.roleSalary);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Role), "CAST ERROR: " + ex.Message);
            }
        }
    }
}