using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class DepartmentDao
    {
        
        private const string TABLE_NAME = "Department";
        public const string ID = "department_id";
        public const string NAME = "department_name";
        public const string MANAGER_ID = "manager_id";

        private DBConnection dbConnection = new DBConnection();

        public List<Department> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }

        public List<Department> GetDepartmentsCanAssignWork(string startTime, string endTime)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} NOT IN (" +
                            $"Select {ProjectAssignmentDao.DEPARTMENT_ID} FROM {ProjectAssignmentDao.TABLE_NAME} " +
                            $"WHERE {ProjectAssignmentDao.PROJECT_ID} IN (" +
                            $"Select {ProjectDao.ID} FROM {ProjectDao.TABLE_NAME}" +
                            $"WHERE {ProjectDao.PROPRESS} <> '100'" +
                            $"AND {ProjectDao.START} <= '{endTime}'" +
                            $"AND {ProjectDao.END} >= '{startTime}'))";
            return dbConnection.GetList(sqlStr, reader => new Department(reader));
        }
    }
}
