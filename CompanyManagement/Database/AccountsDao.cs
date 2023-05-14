using CompanyManagement.Database.Base;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class AccountsDao : BaseDao
    {
        public void Add(Account acc)
        {
            string sqlStr = $"INSERT INTO {accTbl} ({accName}, {accPass}, {accEmplID})" +
                            $"VALUES ('{acc.Username}', '{acc.Password}', '{acc.EmployeeID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string emplID)
        {
            string sqlStr = $"DELETE FROM {accTbl} WHERE {accEmplID}='{emplID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Account acc)
        {
            string sqlStr = $"UPDATE {accTbl} SET {accName}='{acc.Username}', " +
                            $"{accPass}='{acc.Password}' WHERE {accEmplID}='{acc.EmployeeID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Account SearchByUsername(string username)
        {
            string sqlStr = $"SELECT * FROM {accTbl} WHERE {accName}='{username}'";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
    }
}
