using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentDao : BaseDao
    {
        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
