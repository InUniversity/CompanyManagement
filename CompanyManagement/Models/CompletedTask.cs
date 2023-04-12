using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class CompletedTask
    {
        private string id;
        private string taskID;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string TaskID
        {
            get => taskID;
            set => taskID = value;
        }

        public CompletedTask(string id, string taskID)
        {
            this.id = id;
            this.taskID = taskID;
        }
        
        public CompletedTask(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.COMPLETED_TASK_ID];
                taskID = (string)reader[BaseDao.COMPLETED_TASK_TASK_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Information(nameof(CompletedTask), "Error: " + ex.Message);
            }
        }
    }
}
