using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TaskCheckOut
    {
        private string checkInOutID = "";
        private string taskID = "";
        private DateTime updateDate = Utils.EMPTY_DATETIME;
        private string progress;
        private TaskInProject task = new TaskInProject();

        public string CheckInOutID => checkInOutID;
        
        public string TaskID => taskID;
        
        public DateTime UpdateDate => updateDate;
        
        public string Progress => progress;

        public TaskInProject Task
        {
            get => task;
            set => task = value;
        }

        public TaskCheckOut(string checkInOutID, string taskID, DateTime updateDate, string progress, TaskInProject task)
        {
            this.checkInOutID = checkInOutID;
            this.taskID = taskID;
            this.updateDate = updateDate;
            this.progress = progress;
            this.task = task;
        }
        
        public TaskCheckOut(IDataRecord reader)
        {
            try
            {
                checkInOutID = (string)reader[BaseDao.TASK_CHECK_OUT_TASK_ID];
                taskID = (string)reader[BaseDao.TASK_CHECK_OUT_TASK_ID];
                updateDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.TASK_CHECK_OUT_UPDATE_DATE));
                progress = (string)reader[BaseDao.TASK_CHECK_OUT_PROGRESS];
            }
            catch (Exception e)
            {
                Log.Instance.Error(nameof(CheckInOut), "Error: " + e.Message);
            }
        }
    }
}