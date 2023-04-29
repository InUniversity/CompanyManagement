using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class ProjectsDao : BaseDao
    {
        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {PROJECTS_TABLE}({PROJECTS_ID}, {PROJECTS_NAME}, {PROJECTS_DETAILS}, " +
                            $"{PROJECTS_CREATED}, {PROJECTS_START}, {PROJECTS_END}, {PROJECTS_COMPLETED}, " +
                            $"{PROJECTS_PROPRESS}, {PROJECTS_STATUS_ID}, {PROJECTS_OWNER_ID}, {PROJECTS_BONUS_SALARY}) " +
                            $"VALUES ('{project.ID}', N'{project.Name}', N'{project.Details}', " +
                            $"'{Utils.ToSQLFormat(project.CreatedDate)}','{Utils.ToSQLFormat(project.StartDate)}', " +
                            $"'{Utils.ToSQLFormat(project.EndDate)}', '{Utils.ToSQLFormat(project.CompletedDate)}', " +
                            $"'{project.Progress}', '{project.StatusID}', '{project.OwnerID}', '{project.BonusSalary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {PROJECTS_TABLE} WHERE {PROJECTS_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project project)
        {
            string sqlStr = $"UPDATE {PROJECTS_TABLE} SET " +
                            $"{PROJECTS_NAME}=N'{project.Name}', {PROJECTS_DETAILS}=N'{project.Details}', " +
                            $"{PROJECTS_CREATED}='{Utils.ToSQLFormat(project.CreatedDate)}', " +
                            $"{PROJECTS_START}='{Utils.ToSQLFormat(project.StartDate)}', " +
                            $"{PROJECTS_END}='{Utils.ToSQLFormat(project.EndDate)}', " +
                            $"{PROJECTS_COMPLETED}='{Utils.ToSQLFormat(project.CompletedDate)}'," +
                            $"{PROJECTS_PROPRESS}='{project.Progress}', " + $"{PROJECTS_STATUS_ID}='{project.StatusID}', " +
                            $"{PROJECTS_BONUS_SALARY}='{project.BonusSalary}' WHERE {PROJECTS_ID}='{project.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {PROJECTS_TABLE} WHERE {PROJECTS_ID} = '{id}'";
            return (Project)dbConnection.GetSingleObject(sqlStr, reader => new Project(reader));
        }
    }
}
