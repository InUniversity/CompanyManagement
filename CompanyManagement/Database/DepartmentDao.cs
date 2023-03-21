using System.Collections.Generic;
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

        public List<Department> GetDepartmentsCanAssignWork(string startTime, string endTime)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} NOT IN (" +
                            $"Select {PROJECT_ASSIGNMENT_DEPARTMENT_ID} FROM {PROJECT_ASSIGNMENT_TABLE} " +
                            $"WHERE {PROJECT_ASSIGNMENT_PROJECT_ID} IN (" +
                            $"Select {PROJECT_ID} FROM {PROJECT_TABLE}" +
                            $"WHERE {PROJECT_PROPRESS} <> '100'" +
                            $"AND {PROJECT_START} <= '{endTime}'" +
                            $"AND {PROJECT_END} >= '{startTime}'))";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
