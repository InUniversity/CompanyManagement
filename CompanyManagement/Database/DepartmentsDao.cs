using System.Collections.Generic;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentsDao : BaseDao
    {
        public void Add(Department dept)
        {
            //TODO
            string sqlStr = $"INSERT INTO {deptTbl} ({deptID}, {deptName}, {deptHead})" +
                            $"VALUES ('{dept.ID}', '{dept.Name}', '{dept.DeptHeadID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {deptTbl} WHERE {deptID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department dept)
        {
            //TODO
            string sqlStr = $"UPDATE {deptTbl} SET {deptName} = '{dept.Name}', " +
                            $"{deptHead} = '{dept.DeptHeadID}' WHERE {deptID} = '{dept.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {deptTbl}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public Department SearchByID(string dptID)
        {
            string sqlStr = $"SELECT * FROM {deptTbl} WHERE {deptID} = '{dptID}'";
            return (Department)dbConnection.GetSingleObject(sqlStr, reader => new Department(reader));
        }
    }
}
