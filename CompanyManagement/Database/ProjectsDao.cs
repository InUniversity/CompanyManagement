using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Database
{
    public class ProjectsDao : BaseDao
    {
        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {projTbl}({projID}, {projName}, {projDetails}, " +
                            $"{projCreated}, {projStart}, {projEnd}, {projCompleted}, " +
                            $"{projProgress}, {projStatusID}, {projOwnerID}, {projBonus}) " +
                            $"VALUES ('{project.ID}', N'{project.Name}', N'{project.Details}', " +
                            $"'{Utils.ToSQLFormat(project.CreatedDate)}','{Utils.ToSQLFormat(project.StartDate)}', " +
                            $"'{Utils.ToSQLFormat(project.EndDate)}', '{Utils.ToSQLFormat(project.CompletedDate)}', " +
                            $"'{project.Progress}', '{project.StatusID}', '{project.OwnerID}', '{project.BonusSalary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {projTbl} WHERE {projID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project project)
        {
            string sqlStr = $"UPDATE {projTbl} SET " +
                            $"{projName}=N'{project.Name}', {projDetails}=N'{project.Details}', " +
                            $"{projCreated}='{Utils.ToSQLFormat(project.CreatedDate)}', " +
                            $"{projStart}='{Utils.ToSQLFormat(project.StartDate)}', " +
                            $"{projEnd}='{Utils.ToSQLFormat(project.EndDate)}', " +
                            $"{projCompleted}='{Utils.ToSQLFormat(project.CompletedDate)}'," +
                            $"{projProgress}='{project.Progress}', " + $"{projStatusID}='{project.StatusID}', " +
                            $"{projBonus}='{project.BonusSalary}' WHERE {projID}='{project.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {projTbl} WHERE {projID} = '{id}'";
            return (Project)dbConnection.GetSingleObject(sqlStr, reader => new Project(reader));
        }
    }
}
