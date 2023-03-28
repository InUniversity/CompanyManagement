using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Implementations
{
    public class ProjectStatusDao : BaseDao, IProjectStatusDao
    {
        public void Add(ProjectStatus projectStatus)
        {
            string sqlStr = $"INSERT INTO {PROJECT_STATUS_TABLE} ('{PROJECT_STATUS_ID}', '{PROJECT_STATUS_NAME}') " +
                            $"VALUES ({projectStatus.ID}, {projectStatus.Name})";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string projectStatusID)
        {
            string sqlStr = $"DELETE FROM {PROJECT_STATUS_TABLE} WHERE {PROJECT_STATUS_ID} = '{projectStatusID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public List<ProjectStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {PROJECT_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new ProjectStatus(reader));
        }

        public void Update(ProjectStatus projectStatus)
        {
            string sqlStr = $"UPDATE {PROJECT_STATUS_TABLE} SET {PROJECT_STATUS_NAME} = '{projectStatus.Name}' " +
                            $"WHERE {PROJECT_STATUS_ID} = '{projectStatus.ID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }
    }
}
