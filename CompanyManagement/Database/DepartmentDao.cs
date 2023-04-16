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

        public Department DepartmentByEmployeeID(string dptID)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WEHERE {DEPARTMENT_ID} = '{dptID}'";
            return dbConnection.GetList(sqlStr, reader => new Department(reader))[0];
        }

        public string SearchManagerIDByEmployeeID(string employeeID)
        {
            string sqlStr = $"SELECT * FROM {DEPARTMENT_TABLE} WEHERE {DEPARTMENT_ID} = " +
                $"(SELECT {EMPLOYEE_TABLE}.{EMPLOYEE_DEPARTMENT_ID} FROM {EMPLOYEE_TABLE} " +
                $"WHERE {EMPLOYEE_ID} = '{employeeID}')";
            return dbConnection.GetList(sqlStr, reader => new Department(reader))[0].ManagerID;
        }
    }
}
