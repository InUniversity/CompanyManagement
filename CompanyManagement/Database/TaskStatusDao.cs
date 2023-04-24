using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class TaskStatusDao : BaseDao
    {
        public List<TaskStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TASK_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new TaskStatus(reader));
        }
    }
}
