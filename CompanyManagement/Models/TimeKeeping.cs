using CompanyManagement.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyManagement.Models
{
    public class TimeKeeping
    {
        private string taskID;
        private string startTime;
        private string endTime;
        private string employeeeID;
        private string notes;
        private string createBy;

        public string TaskID
        {
            get => taskID;
            set => taskID = value;
        }

        public string StartTime
        {
            get => startTime;
            set => startTime = value;
        }

        public string EndTime
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

        public TimeKeeping(string taskID, string startTime, string endTime, string employeeeID, string notes, string createBy)
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
                startTime = (string)reader[BaseDao.TIME_KEEPING_START_TIME];
                endTime = (string)reader[BaseDao.TIME_KEEPING_END_TIME];
                employeeeID = (string)reader[BaseDao.TIME_KEEPING_EMPLOYEE_ID];
                notes = (string)reader[BaseDao.TIME_KEEPING_NOTES];
                createBy = (string)reader[BaseDao.TIME_KEEPING_CREATE_BY];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
