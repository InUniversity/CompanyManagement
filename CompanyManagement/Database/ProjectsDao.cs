using CompanyManagement.Database.Base;

namespace CompanyManagement.Database
{
    public class ProjectsDao : BaseDao
    {
        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {PROJECTS_TABLE}({PROJECTS_ID}, {PROJECTS_NAME}, {PROJECTS_CREATED} {PROJECTS_START}, " +
                            $"{PROJECTS_END}, {PROJECTS_COMPLETED}, {PROJECTS_PROPRESS}, {PROJECTS_STATUS_ID}, {PROJECTS_OWNER_ID}) " +
                            $"VALUES ('{project.ID}', N'{project.Name}', '{project.CreatedDate}','{project.StartDate}', '{project.EndDate}', " +
                            $"'{project.CompletedDate}','{project.Progress}', '{project.StatusID}', '{project.OwnerID}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {PROJECTS_TABLE} WHERE {PROJECTS_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project project)
        {
            string sqlStr = $"UPDATE {PROJECTS_TABLE} SET {PROJECTS_NAME}=N'{project.Name}', {PROJECTS_CREATED}='{project.CreatedDate}', {PROJECTS_START} ='{project.StartDate}', " +
                            $"{PROJECTS_END}='{project.EndDate}', {PROJECTS_COMPLETED} = '{project.CompletedDate}',{PROJECTS_PROPRESS}='{project.Progress}', " +
                            $"{PROJECTS_STATUS_ID} = '{project.StatusID}' WHERE {PROJECTS_ID}='{project.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {PROJECTS_TABLE} WHERE {PROJECTS_ID} = '{id}'";
            return (Project)dbConnection.GetSingleObject(sqlStr, reader => new Project(reader));
        }
    }
}
