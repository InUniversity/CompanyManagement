﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public void Add(Department department)
        {
            string sqlStr =
                $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}, {MANAGER_ID}) VALUES ('{department.ID}', N'{department.Name}', '{department.ManagerID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string departmentID)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{departmentID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department deparment)
        {
            string sqlStr = 
                $"UPDATE {TABLE_NAME} SET {NAME}=N'{deparment.Name}', {MANAGER_ID}='{deparment.ManagerID}' WHERE {ID}='{deparment.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetDataTable(sqlStr);
        }

        private List<Department> ToList(SqlDataReader reader)
        {
            List<Department> departments = new List<Department>();

            while (reader.Read())
            {
                Department department = new Department
                {
                    ID = (string)reader[ID],
                    Name = (string)reader[NAME],
                    ManagerID = (string)reader[MANAGER_ID]
                };
                departments.Add(department);
            }
            reader.Close();
            return departments;
        }

        public List<Department> GetDeparmentsCanAssignWork(string startTime, string endTime)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} NOT IN (" +
                            $"Select {ProjectAssignmentDao.DEPARTMENT_ID} FROM {ProjectAssignmentDao.TABLE_NAME} " +
                            $"WHERE {ProjectAssignmentDao.PROJECT_ID} IN (" +
                            $"Select {ProjectDao.ID} FROM {ProjectDao.TABLE_NAME}" +
                            $"WHERE {ProjectDao.PROPRESS} <> '100'" +
                            $"AND {ProjectDao.START} <= '{endTime}'" +
                            $"AND {ProjectDao.END} >= '{startTime}'))";

            SqlDataReader reader = dbConnection.GetDataReader(sqlStr);

            return ToList(reader);
        }
    }
}
