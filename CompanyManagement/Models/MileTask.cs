using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class MileTask
    {
        private string mileID;
        private string taskID;

        public string MileID
        {
            get => mileID;
            set => mileID = value;
        }

        public string TskID
        {
            get => taskID;
            set => taskID = value;
        }

        public MileTask(string id, string taskID)
        {
            this.mileID = id;
            this.taskID = taskID;
        }

        public MileTask(IDataRecord record)
        {
            try
            {
                mileID = Utils.GetString(record, BaseDao.mileTskID);
                taskID = Utils.GetString(record, BaseDao.mileTskTskID);
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(MileTask), "CAST ERROR: " + ex.Message);
            }
        }
    }
}