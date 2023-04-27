using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class Roles
    {
        private string id;
        private string title;

        public string ID => id;
        public string Title => title;

        public Roles(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.ROLES_ID];
                title = (string)reader[BaseDao.ROLES_NAME];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Roles), "CAST ERROR: " + ex.Message);
            }
        }
    }
}