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

        public string ID => id;
        public string Title => title;

        public Role() { }
        
        public Role(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.roleID];
                title = (string)reader[BaseDao.roleName];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Role), "CAST ERROR: " + ex.Message);
            }
        }
    }
}