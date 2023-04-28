using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class ProjectStatusesDao : BaseDao
    {
        public List<ProjectStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {PROJECT_STATUSES_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new ProjectStatus(reader));
        }
    }
}
