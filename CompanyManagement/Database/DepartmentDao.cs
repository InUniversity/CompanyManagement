using System.Data;
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

        public void Add(Department dep)
        {
            string sqlStr =
                $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}, {MANAGER_ID}) VALUES ('{dep.ID}', N'{dep.Name}', '{dep.ManagerID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Save(Department dep)
        {
            string sqlStr = 
                $"UPDATE {TABLE_NAME} SET {NAME}=N'{dep.Name}', {MANAGER_ID}='{dep.ManagerID}' WHERE {ID}='{dep.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetDataTable(sqlStr);
        }
    }
}
