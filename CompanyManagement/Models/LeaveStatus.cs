using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;

namespace CompanyManagement.Models
{
    public class LeaveStatus
    {
        private string leaveStatusID = "";
        private string leaveStatusName = "";

        public string LeaveStatusID => leaveStatusID;
        public string LeaveStatusName => leaveStatusName;

        public LeaveStatus(IDataRecord reader)
        {
            try
            {
                leaveStatusID = (string)reader[BaseDao.LEAVE_STATUS_ID];
                leaveStatusName = (string)reader[BaseDao.LEAVE_STATUSES_NAME];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
