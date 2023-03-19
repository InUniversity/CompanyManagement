using System.Collections.Generic;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    class AccountDao : IDao
    {
        private DBConnection dbConnection = new DBConnection();

        public void Add(Account account)
        {
            string sqlStr = $"INSERT INTO {ACCOUNT_TABLE} ({ACCOUNT_USERNAME}, {ACCOUNT_PASSWORD}, {ACCOUNT_EMPLOYEE_ID}) " +
                            $"VALUES ('{account.Username}', '{account.Password}', '{account.EmployeeId}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(Account account)
        {
            string sqlStr = $"DELETE FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_USERNAME}='{account.Username}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account account)
        {
            string sqlStr = $"UPDATE {ACCOUNT_TABLE} SET {ACCOUNT_PASSWORD}='{account.Password}' WHERE {ACCOUNT_USERNAME}='{account.Username}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByEmployeeID(string employeeId)
        {
            string sqlStr = $"SELECT * FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_EMPLOYEE_ID}='{employeeId}'";
            List<Account> accounts  = dbConnection.GetList(sqlStr, reader => new Account(reader));
            if (accounts.Count == 0)
                return null;
            return accounts[0];
        }
        
        public Account SearchByUsername(string username)
        {
            string sqlStr = $"SELECT * FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_USERNAME}='{username}'";
            List<Account> accounts  = dbConnection.GetList(sqlStr, reader => new Account(reader));
            if (accounts.Count == 0)
                return null;
            return accounts[0];
        }
    }
}
