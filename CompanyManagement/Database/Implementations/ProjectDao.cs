using System.Collections.Generic;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectDao : BaseDao, IProjectDao
    {
        public void Add(Project project)
        {
            string sqlStr = $"INSERT INTO {PROJECT_TABLE}({PROJECT_ID}, {PROJECT_NAME}, {PROJECT_START}, " +
                            $"{PROJECT_END}, {PROJECT_PROPRESS}) VALUES ('{project.ID}', N'{project.Name}', " +
                            $"'{project.Start}', '{project.End}', '{project.Progress}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {PROJECT_TABLE} WHERE {PROJECT_ID} = '{id}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Project project)
        {
            string sqlStr = $"UPDATE {PROJECT_TABLE} SET {PROJECT_NAME}=N'{project.Name}', {PROJECT_START} ='{project.Start}', " +
                            $"{PROJECT_END}='{project.End}', {PROJECT_PROPRESS}='{project.Progress}' WHERE {PROJECT_ID}='{project.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<Project> GetAll()
        {
            string sqlStr = $"SELECT * FROM {PROJECT_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new Project(reader));
        }
       
        public Project SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {PROJECT_TABLE} WHERE {PROJECT_ID} = '{id}'";
            List<Project> projects = dbConnection.GetList(sqlStr, reader => new Project(reader));
            if (projects.Count == 0)
                return null;
            return projects[0];
        }
    }
}
