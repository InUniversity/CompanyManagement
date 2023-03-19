using System.Collections.Generic;
using System.Data;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    class AccountDao
    {

        private const string TABLE_NAME = "Account";
        public const string USERNAME = "account_username";
        public const string PASSWORD = "account_password";
        public const string EMPLOYEE_ID = "employee_id";

        private DBConnection dbConnection = new DBConnection();

        public void Add(Account account)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME} ({USERNAME}, {PASSWORD}, {EMPLOYEE_ID}) " +
                            $"VALUES ('{account.Username}', '{account.Password}', '{account.EmployeeId}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(Account account)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {USERNAME} = {account.Username}";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account account)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {PASSWORD} = '{account.Password}' WHERE {USERNAME} = '{account.Username}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByEmployeeID(string employeeId)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {EMPLOYEE_ID} = '{employeeId}'";
            List<Account> accounts  = dbConnection.GetList(sqlStr, reader => new Account(reader));
            if (accounts.Count == 0)
                return null;
            return accounts[0];
        }
        
        public Account SearchByUsername(string username)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {USERNAME} = '{username}'";
            List<Account> accounts  = dbConnection.GetList(sqlStr, reader => new Account(reader));
            if (accounts.Count == 0)
                return null;
            return accounts[0];
        }
    }
}
