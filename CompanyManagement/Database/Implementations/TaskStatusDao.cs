using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using System.Collections.Generic;

namespace CompanyManagement.Database.Implementations
{
    public class TaskStatusDao: BaseDao, ITaskStatusDao
    {
        public void Add(TaskStatus taskStatus)
        {
            string sqlStr = $"INSERT INTO {TASK_STATUS_TABLE} ({TASK_STATUS_ID}, {TASK_STATUS_NAME})" +
                            $"VALUE('{taskStatus.ID}', '{taskStatus.Name}')";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string taskStatusID)
        {
            string sqlStr = $"DELETE FROM {TASK_STATUS_TABLE} WHERE {TASK_STATUS_ID} = '{taskStatusID}'";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(TaskStatus taskStatus)
        {
            string sqlStr = $"UPDATE {TASK_STATUS_TABLE} SET {TASK_STATUS_NAME} = '{taskStatus.Name}', " +
                            $"WHERE {TASK_STATUS_ID} = '{taskStatus.ID}';";
            dbConnection.ExecuteNonQuery(sqlStr);
        }

        public TaskStatus SearchByID(string taskStatusID)
        {
            string sqlStr = $"SELECT * FROM {TASK_STATUS_TABLE} WHERE {TASK_STATUS_ID} = '{taskStatusID}'";
            List<TaskStatus> tasks = dbConnection.GetList(sqlStr, reader => new TaskStatus(reader));
            if (tasks.Count == 0)
                return null;
            return tasks[0];
        }

        public List<TaskStatus> GetAll()
        {
            string sqlStr = $"SELECT * FROM {TASK_STATUS_TABLE}";
            return dbConnection.GetList(sqlStr, reader => new TaskStatus(reader));
        }
    }
}
