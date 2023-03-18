using System.Data;
using CompanyManagement.Models;

namespace CompanyManagement.Database
{
    public class PositionDao
    {
        
        private const string TABLE_NAME = "Position";
        public const string ID = "position_id";
        public const string NAME = "position_name";

        private DBConnection dbConnection = new DBConnection();

        public void Add(PositionInCompany pos)
        {
            string sqlStr =
                $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}) VALUES ('{pos.ID}', N'{pos.Name}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Department dep)
        {
            string sqlStr = 
                $"UPDATE {TABLE_NAME} SET {NAME}=N'{dep.Name}' WHERE {ID}='{dep.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetDataTable(sqlStr);
        }
    }
}
