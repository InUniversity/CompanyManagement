using System;
using System.Data.SqlClient;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class CheckInOut
    {
        private string id = "";
        private string employeeID = "";
        private DateTime checkInTime = Utils.EMPTY_DATETIME;
        private DateTime checkOutTime = Utils.EMPTY_DATETIME;
        private bool checkOutStatus = false;
        private string taskID = "";

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public DateTime CheckInTime
        {
            get => checkInTime;
            set => checkInTime = value;
        }

        public DateTime CheckOutTime
        {
            get => checkOutTime;
            set => checkOutTime = value;
        }

        public bool CheckOutStatus
        {
            get => checkOutStatus;
            set => checkOutStatus = value;
        }

        public string TaskID
        {
            get => taskID;
            set => taskID = value;
        }

        public CheckInOut() { }

        public CheckInOut(string id, string employeeID, DateTime checkInTime, DateTime checkOutTime, 
            bool checkOutStatus, string taskID)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.checkInTime = checkInTime;
            this.checkOutTime = checkOutTime;
            this.checkOutStatus = checkOutStatus;
            this.taskID = taskID;
        }

        public CheckInOut(string id, string employeeID, DateTime checkInTime, string taskID)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.checkInTime = checkInTime;
            this.taskID = taskID;
        }

        public CheckInOut(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.CHECK_IN_OUT_ID];
                employeeID = (string)reader[BaseDao.CHECK_IN_OUT_EMPLOYEE_ID];
                checkInTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.CHECK_IN_TIME));
                checkOutTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.CHECK_OUT_TIME));
                checkOutStatus = (bool)reader[BaseDao.CHECK_OUT_STATUS];
                taskID = (string)reader[BaseDao.CHECK_IN_OUT_TASK_ID];
            }
            catch (Exception e)
            {
                Log.Instance.Error(nameof(CheckInOut), "Error: " + e.Message);
            }
        }
    }
}