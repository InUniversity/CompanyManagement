using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class DepartmentDao : BaseDao, IDepartmentDao
    {
        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
