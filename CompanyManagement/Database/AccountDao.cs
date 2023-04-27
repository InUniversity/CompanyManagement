using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class AccountDao : BaseDao
    {
        public void Add(Account account)
        {
            string sqlStr = $"INSERT INTO {ACCOUNTS_TABLE} ({ACCOUNTS_USERNAME}, {ACCOUNTS_USERNAME}, {ACCOUNTS_EMPLOYEE_ID})" +
                            $"VALUES ({account.Username}, {account.Password}, {account.EmployeeID})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string employeeID)
        {
            string sqlStr = $"DELETE FROM {ACCOUNTS_TABLE} WHERE {ACCOUNTS_EMPLOYEE_ID}='{employeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account account)
        {
            string sqlStr = $"UPDATE {ACCOUNTS_TABLE} SET {ACCOUNTS_USERNAME}='{account.Username}', " +
                            $"{ACCOUNTS_PASSWORD}='{account.Password}' WHERE {ACCOUNTS_EMPLOYEE_ID}='{account.EmployeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByUsername(string userName)
        {
            string sqlStr = $"SELECT * FROM {ACCOUNTS_TABLE} WHERE {ACCOUNTS_USERNAME}='{userName}'";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
    }
}
