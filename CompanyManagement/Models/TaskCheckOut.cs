using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TaskCheckOut
    {
        private string timeShtID = "";
        private string taskID = "";
        private DateTime update = Utils.emptyDate;
        private string progress;
        private TaskInProject task = new TaskInProject();

        public string TimeShtID => timeShtID;
        
        public string TaskID => taskID;
        
        public DateTime Update => update;
        
        public string Progress => progress;

        public TaskInProject Task
        {
            get => task;
            set => task = value;
        }

        public TaskCheckOut(string timeShtID, string taskID, DateTime update, string progress, TaskInProject task)
        {
            this.timeShtID = timeShtID;
            this.taskID = taskID;
            this.update = update;
            this.progress = progress;
            this.task = task;
        }
        
        public TaskCheckOut(IDataRecord record)
        {
            try
            {
                timeShtID = Utils.GetString(record, BaseDao.taskOutTimeShtID);
                taskID = Utils.GetString(record, BaseDao.taskOutTaskID);
                update = Utils.GetDateTime(record, BaseDao.taskOutUpdate);
                progress = Utils.GetString(record, BaseDao.taskOutProgress);
            }
            catch (Exception e)
            {
                Log.Ins.Error(nameof(TimeSheet), "Error: " + e.Message);
            }
        }
    }
}