using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    class AccountDao
    {

        public const string TABLE_NAME = "Account";
        public const string USERNAME = "account_username";
        public const string PASSWORD = "account_password";
        public const string EMPLOYEE_ID = "employee_id";

        private DBConnection dbConnection = new DBConnection();

        public void Add(Account acc)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME} ({USERNAME}, {PASSWORD}, {EMPLOYEE_ID}) " +
                            $"VALUES ('{acc.Username}', '{acc.Password}', '{acc.EmployeeId}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Account acc)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {PASSWORD} = '{acc.Password}' WHERE {USERNAME} = '{acc.Username}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByID(string employeeId)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {EMPLOYEE_ID} = '{employeeId}'";
            DataTable dataTable = dbConnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Account(dataTable.Rows[0]);
        }
        
        public Account SearchByUsername(string username)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {USERNAME} = '{username}'";
            DataTable dataTable = dbConnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Account(dataTable.Rows[0]);
        }
    }
}
