using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class TaskStatus
    {
        private string id;
        private string name;

        public string ID => id;
        public string Name => name;

        public TaskStatus(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.taskStasID];
                name = (string)reader[BaseDao.taskStasName];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(TaskStatus), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
