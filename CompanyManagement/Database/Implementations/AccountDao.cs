using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;

namespace CompanyManagement.Database.Implementations
{
    public class AccountDao : BaseDao, IAccountDao
    {
        public void Add(Account account)
        {
            string sqlStr = $"INSERT INTO {ACCOUNT_TABLE} ({ACCOUNT_USERNAME}, {ACCOUNT_USERNAME}, {ACCOUNT_EMPLOYEE_ID})" +
                            $"VALUES ({account.Username}, {account.Password}, {account.EmployeeID})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string employeeID)
        {
            string sqlStr = $"DELETE FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_EMPLOYEE_ID}='{employeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account account)
        {
            string sqlStr = $"UPDATE {ACCOUNT_TABLE} SET {ACCOUNT_USERNAME}='{account.Username}', " +
                            $"{ACCOUNT_PASSWORD}='{account.Password}' WHERE {ACCOUNT_EMPLOYEE_ID}='{account.EmployeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByUsername(string userName)
        {
            string sqlStr = $"SELECT * FROM {ACCOUNT_TABLE} WHERE {ACCOUNT_USERNAME}='{userName}'";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
    }
}
