using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentsDao : BaseDao
    {
        public void Add(Department department)
        {
            string sqlStr = $"INSERT INTO {deptTbl} ({deptID}, {deptName}, {deptHead})" +
                            $"VALUES ('{department.ID}', '{department.Name}', '{department.DepartmentHeadID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {deptTbl} WHERE {deptID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department department)
        {
            string sqlStr = $"UPDATE {deptTbl} SET {deptName} = '{department.Name}', " +
                            $"{deptHead} = '{department.DepartmentHeadID}' WHERE {deptID} = '{department.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {deptTbl}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public Department DepartmentByEmployeeDeptID(string dptID)
        {
            string sqlStr = $"SELECT * FROM {deptTbl} WHERE {deptID} = '{dptID}'";
            return (Department)dbConnection.GetSingleObject(sqlStr, reader => new Department(reader));
        }
    }
}
