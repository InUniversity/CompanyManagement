using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class AccountsDao : BaseDao
    {
        public void Add(Account account)
        {
            string sqlStr = $"INSERT INTO {accTbl} ({accName}, {accName}, {accEmplID})" +
                            $"VALUES ({account.Username}, {account.Password}, {account.EmployeeID})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string employeeID)
        {
            string sqlStr = $"DELETE FROM {accTbl} WHERE {accEmplID}='{employeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account account)
        {
            string sqlStr = $"UPDATE {accTbl} SET {accName}='{account.Username}', " +
                            $"{accPass}='{account.Password}' WHERE {accEmplID}='{account.EmployeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByUsername(string userName)
        {
            string sqlStr = $"SELECT * FROM {accTbl} WHERE {accName}='{userName}'";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
    }
}
