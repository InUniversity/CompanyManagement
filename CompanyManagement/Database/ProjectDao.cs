using System.Data;

namespace CompanyManagement.Database
{
    public class ProjectDao
    {

        public const string TABLE_NAME = "Project";
        public const string ID = "project_id";
        public const string NAME = "project_name";
        public const string START = "create_time";
        public const string END = "end_time";
        public const string PROPRESS = "progress";

        DBConnection dbConnection = new DBConnection();

        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {TABLE_NAME}({ID}, {NAME}, {START}, {END}, {PROPRESS})" +
                $"VALUES ('{project.ID}', N'{project.Name}', '{project.Start}', '{project.End}', '{project.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
        
        public void DeleteAll()
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME}";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project project)
        {
            string sqlStr = $"UPDATE {TABLE_NAME} SET {NAME}=N'{project.Name}', {START} ='{project.Start}', " +
                            $"{END}='{project.End}', {PROPRESS}='{project.Progress}' WHERE {ID}='{project.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbConnection.GetDataTable(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dataTable = dbConnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Project(dataTable.Rows[0]);
        }
    }
}
