using System.Windows;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class AccountsDao : BaseDao
    {
        public void Add(Employee empl)
        {
            string sqlStr = $"INSERT INTO {accTbl} ({accName}, {accPass}, {accEmplID})" +
                            $"VALUES (CONCAT('{empl.ID}', SUBSTRING(CONVERT(varchar, '{Utils.ToSQLFormat(empl.Birthday)}', 103), 9, 2), " +
                            $"SUBSTRING(CONVERT(varchar, '{Utils.ToSQLFormat(empl.Birthday)}', 103), 6, 2)), '@1234567', '{empl.ID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
            MessageBox.Show("completed add");
        }

        public void Delete(string emplID)
        {
            string sqlStr = $"DELETE FROM {accTbl} WHERE {accEmplID}='{emplID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
            MessageBox.Show("completed delete");
        }

        public void Update(Employee empl)
        {
            Delete(empl.ID);
            Add(empl);
            MessageBox.Show("completed update");
        }

        public Account SearchByUsername(string username)
        {
            string sqlStr = $"SELECT * FROM {accTbl} WHERE {accName}='{username}'";
            return (Account)dbConnection.GetSingleObject(sqlStr, reader => new Account(reader));
        }
    }
}
