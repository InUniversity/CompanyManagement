using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class RolesDao : BaseDao
    {
        public List<Roles> GetAll()
        {
            string sqlStr = $"SELECT * FROM {ROLES_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Roles(reader));
        }
    }
}
