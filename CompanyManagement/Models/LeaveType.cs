using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data.SqlClient;

namespace CompanyManagement.Models
{
    public class LeaveType
    {
        private string leaveTypeID = "";
        private string leaveTypeName = "";

        public string ID
        { 
            get { return leaveTypeID; } 
            set { leaveTypeID = value; } 
        }

        public string Name
        { 
            get { return leaveTypeName; } 
            set { leaveTypeName = value; } 
        }

        public LeaveType() { }

        public LeaveType(string leaveTypeID, string leaveTypeName)
        {
            this.leaveTypeID = leaveTypeID;
            this.leaveTypeName = leaveTypeName;
        }

        public LeaveType(SqlDataReader reader)
        {
            try
            {
                leaveTypeID = (string)reader[BaseDao.LEAVE_TYPE_ID];
                leaveTypeName = (string)reader[BaseDao.LEAVE_TYPE_NAME];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
