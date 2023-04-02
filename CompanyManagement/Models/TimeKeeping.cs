using CompanyManagement.Database;
using System;
using System.Data.SqlClient;
using System.Windows;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TimeKeeping
    {
        private string taskID = "";
        private TimeOnly startTime = TimeOnly.MinValue;
        private TimeOnly endTime = TimeOnly.MinValue;
        private string employeeeID = "";
        private string notes = "";
        private string createBy = "";

        public string TaskID
        {
            get => taskID;
            set => taskID = value;
        }

        public TimeOnly StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        public TimeOnly EndTime
        {
            get => endTime;
            set => endTime = value;
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

        public TimeKeeping(string taskID, TimeOnly startTime, TimeOnly endTime, string employeeeID, string notes, string createBy)
        {
            this.taskID = taskID;
            this.startTime = startTime;
            this.endTime = endTime;
            this.employeeeID = employeeeID;
            this.notes = notes;
            this.createBy = createBy;
        }

        public TimeKeeping(SqlDataReader reader)
        {
            try
            {
                taskID = (string)reader[BaseDao.TIME_KEEPING_TASK_ID];
                startTime = Utils.TimeSpanToTimeOnly(reader.GetTimeSpan(reader.GetOrdinal(BaseDao.TIME_KEEPING_START_TIME)));
                endTime = Utils.TimeSpanToTimeOnly(reader.GetTimeSpan(reader.GetOrdinal(BaseDao.TIME_KEEPING_END_TIME)));
                employeeeID = (string)reader[BaseDao.TIME_KEEPING_EMPLOYEE_ID];
                notes = (string)reader[BaseDao.TIME_KEEPING_NOTES];
                createBy = (string)reader[BaseDao.TIME_KEEPING_CREATE_BY];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}