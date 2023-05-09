using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System.Windows.Documents;

namespace CompanyManagement.Database
{
    public class ProjectsDao : BaseDao
    {
        public void Add(Project proj)
        {
            string sqlStr = $"INSERT INTO {projTbl}({projID}, {projName}, {projDetails}, " +
                            $"{projCreated}, {projStart}, {projEnd}, {projCompleted}, " +
                            $"{projProgress}, {projStatusID}, {projOwnerID}, {projBonus}) " +
                            $"VALUES ('{proj.ID}', N'{proj.Name}', N'{proj.Details}', " +
                            $"'{Utils.ToSQLFormat(proj.CreatedDate)}','{Utils.ToSQLFormat(proj.StartDate)}', " +
                            $"'{Utils.ToSQLFormat(proj.EndDate)}', '{Utils.ToSQLFormat(proj.CompletedDate)}', " +
                            $"'{proj.Progress}', '{proj.StatusID}', '{proj.OwnerID}', '{proj.BonusSalary}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {projTbl} WHERE {projID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project proj)
        {
            string sqlStr = $"UPDATE {projTbl} SET " +
                            $"{projName}=N'{proj.Name}', {projDetails}=N'{proj.Details}', " +
                            $"{projCreated}='{Utils.ToSQLFormat(proj.CreatedDate)}', " +
                            $"{projStart}='{Utils.ToSQLFormat(proj.StartDate)}', " +
                            $"{projEnd}='{Utils.ToSQLFormat(proj.EndDate)}', " +
                            $"{projCompleted}='{Utils.ToSQLFormat(proj.CompletedDate)}'," +
                            $"{projProgress}='{proj.Progress}', " + $"{projStatusID}='{proj.StatusID}', " +
                            $"{projBonus}='{proj.BonusSalary}' WHERE {projID}='{proj.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {projTbl} WHERE {projID} = '{id}'";
            return (Project)dbConnection.GetSingleObject(sqlStr, reader => new Project(reader));
        }
    }
}
