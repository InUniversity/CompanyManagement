using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class ProjectStatusesDao : BaseDao
    {
        public List<ProjectStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {projStasTbl}";
            return dbConnection.GetList(sqlStr, reader => new ProjectStatus(reader));
        }
    }
}
