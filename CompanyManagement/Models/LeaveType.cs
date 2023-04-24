using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class LeaveType
    {
        private string leaveTypeID = "";
        private string leaveTypeName = "";

        public string LeaveTypeID => leaveTypeID;
        public string LeaveTypeName => leaveTypeName;

        public LeaveType(IDataRecord reader)
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
