using System;
using System.Data;
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
        private string taskCheckInID = "";

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

        public string TaskCheckInID
        {
            get => taskCheckInID;
            set => taskCheckInID = value;
        }

        public CheckInOut() { }

        public CheckInOut(string id, string employeeID, DateTime checkInTime, DateTime checkOutTime, string taskCheckInID)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.checkInTime = checkInTime;
            this.checkOutTime = checkOutTime;
            this.taskCheckInID = taskCheckInID;
        }

        public CheckInOut(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.CHECK_IN_OUT_ID];
                employeeID = (string)reader[BaseDao.CHECK_IN_OUT_EMPLOYEE_ID];
                checkInTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.CHECK_IN_OUT_CHECK_IN_TIME));
                checkOutTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.CHECK_IN_OUT_CHECK_OUT_TIME));
                taskCheckInID = (string)reader[BaseDao.CHECK_IN_OUT_TASK_CHECK_IN_ID];
            }
            catch (Exception e)
            {
                Log.Instance.Error(nameof(CheckInOut), "Error: " + e.Message);
            }
        }
    }
}