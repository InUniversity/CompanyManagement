using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Role
    {
        private string id;
        private string title;
        private decimal baseSalary;

        public string ID => id;
        public string Title => title;
        public decimal BaseSalary => baseSalary;

        public Role() { }
        
        public Role(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.roleID);
                title = Utils.GetString(record, BaseDao.roleTitle);
                baseSalary = Utils.GetDecimal(record, BaseDao.roleSalary);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Role), "CAST ERROR: " + ex.Message);
            }
        }
    }
}