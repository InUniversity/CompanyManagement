using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database
{
    public class TaskStatusDao : BaseDao
    {
        public TaskStatus SearchByID(string taskStatusID)
        {
            string sqlStr = $"SELECT * FROM {TASK_STATUS_TABLE} WHERE {TASK_STATUS_ID} = '{taskStatusID}'";        
            return (TaskStatus)dbConnection.GetSingleObject(sqlStr, reader => new TaskStatus(reader));
        }

        public List<TaskStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TASK_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new TaskStatus(reader));
        }
    }
}
