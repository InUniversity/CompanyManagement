using System;
using System.Data;
using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;

namespace CompanyManagement.Models
{
    public class TimeSheet
    {
        private string id = "";
        private string employeeID = "";
        private DateTime checkInTime = Utils.emptyDate;
        private DateTime checkOutTime = Utils.emptyDate;
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

        public TimeSheet() { }

        public TimeSheet(string id, string employeeID, DateTime checkInTime, DateTime checkOutTime, string taskCheckInID)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.checkInTime = checkInTime;
            this.checkOutTime = checkOutTime;
            this.taskCheckInID = taskCheckInID;
        }

        public TimeSheet(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.timeShtID];
                employeeID = (string)reader[BaseDao.timeShtEmplID];
                checkInTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.timeShtInTime));
                checkOutTime = reader.GetDateTime(reader.GetOrdinal(BaseDao.timeShtOutTime));
                taskCheckInID = (string)reader[BaseDao.timeShtTaskInID];
            }
            catch (Exception e)
            {
                Log.Instance.Error(nameof(TimeSheet), "Error: " + e.Message);
            }
        }
    }
}