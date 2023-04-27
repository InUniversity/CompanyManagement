using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentDao : BaseDao
    {
        public void Add(Department department)
        {
            string sqlStr = $"INSERT INTO {DEPARTMENTS_TABLE} ({DEPARTMENTS_ID}, {DEPARTMENTS_NAME}, {DEPARTMENTS_MANAGER_ID})" +
                            $"VALUES ('{department.ID}', '{department.Name}', '{department.ManagerID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {DEPARTMENTS_TABLE} WHERE {DEPARTMENTS_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department department)
        {
            string sqlStr = $"UPDATE {DEPARTMENTS_TABLE} SET {DEPARTMENTS_NAME} = '{department.Name}', " +
                            $"{DEPARTMENTS_MANAGER_ID} = '{department.ManagerID}' WHERE {DEPARTMENTS_ID} = '{department.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENTS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public Department DepartmentByEmployeeDeptID(string dptID)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENTS_TABLE} WHERE {DEPARTMENTS_ID} = '{dptID}'";
            return (Department)dbConnection.GetSingleObject(sqlStr, reader => new Department(reader));
        }
    }
}
