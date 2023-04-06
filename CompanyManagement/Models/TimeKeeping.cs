using System;
using System.Data.SqlClient;
using CompanyManagement.Utilities;
using CompanyManagement.Database.Base;

namespace CompanyManagement.Models
{
    public class TimeKeeping
    {
        private string taskID = "";
        private DateTime start = DateTime.Now;
        private DateTime end = DateTime.Now;
        private string employeeeID = "";
        private string notes = "";
        private string createBy = "";

        public string TaskID
        {
            get => taskID;
            set => taskID = value;
        }

        public DateTime Start
        {
            get => start;
            set => start = value;
        }

        public DateTime End
        {
            get => end;
            set => end = value;
        }

        public string EmployeeID
        {
            get => employeeeID;
            set => employeeeID = value;
        }

        public string Notes
        {
            get => notes;
            set => notes = value;
        }

        public string CreateBy
        {
            get => createBy;
            set => createBy = value;
        }

        public TimeKeeping() { }

        public TimeKeeping(string taskID, DateTime startTime, DateTime endTime, string employeeeID, string notes, string createBy)
        {
            this.taskID = taskID;
            this.start = startTime;
            this.end = endTime;
            this.employeeeID = employeeeID;
            this.notes = notes;
            this.createBy = createBy;
        }

        public TimeKeeping(SqlDataReader reader)
        {
            try
            {
                taskID = (string)reader[BaseDao.TIME_KEEPING_TASK_ID];
                start = reader.GetDateTime(reader.GetOrdinal(BaseDao.TIME_KEEPING_START_TIME));
                end = reader.GetDateTime(reader.GetOrdinal(BaseDao.TIME_KEEPING_END_TIME));
                employeeeID = (string)reader[BaseDao.TIME_KEEPING_EMPLOYEE_ID];
                notes = (string)reader[BaseDao.TIME_KEEPING_NOTES];
                createBy = (string)reader[BaseDao.TIME_KEEPING_CREATE_BY];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(TimeKeeping), "CAST ERROR: " + ex.Message);
            }
        }
    }
}