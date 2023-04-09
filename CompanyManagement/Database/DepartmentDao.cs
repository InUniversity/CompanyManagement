using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentDao : BaseDao
    {
        public void Add(Department department)
        {
            string sqlStr = $"INSERT INTO {DEPARTMENT_TABLE} ({DEPARTMENT_ID}, {DEPARTMENT_NAME}, {DEPARTMENT_MANAGER_ID})" +
                            $"VALUES ('{department.ID}', '{department.Name}', '{department.ManagerID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {DEPARTMENT_TABLE} WHERE {DEPARTMENT_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department department)
        {
            string sqlStr = $"UPDATE {DEPARTMENT_TABLE} SET {DEPARTMENT_NAME} = '{department.Name}', " +
                            $"{DEPARTMENT_MANAGER_ID} = '{department.ManagerID}' WHERE {DEPARTMENT_ID} = '{department.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
