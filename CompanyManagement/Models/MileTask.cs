using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class MileTask
    {
        private string id;
        private string taskID;

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string TskID
        {
            get => taskID;
            set => taskID = value;
        }

        public MileTask(string id, string taskID)
        {
            this.id = id;
            this.taskID = taskID;
        }

        public MileTask(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.mileTskID);
                taskID = Utils.GetString(record, BaseDao.mileTskTskID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(MileTask), "CAST ERROR: " + ex.Message);
            }
        }
    }
}