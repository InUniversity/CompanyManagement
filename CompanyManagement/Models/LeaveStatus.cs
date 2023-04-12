using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Models
{
    public class LeaveStatus
    {
        private string leaveStatusID = "";
        private string leaveStatusName = "";

        public string ID
        {
            get { return leaveStatusID; }
            set { leaveStatusID = value; }
        }

        public string Name
        {
            get { return leaveStatusName; }
            set { leaveStatusName = value; }
        }

        public LeaveStatus() { }

        public LeaveStatus(string leaveStatusID, string leaveStatusName)
        {
            this.leaveStatusID = leaveStatusID;
            this.leaveStatusName = leaveStatusName;
        }

        public LeaveStatus(SqlDataReader reader)
        {
            try
            {
                leaveStatusID = (string)reader[BaseDao.LEAVE_STATUS_ID];
                leaveStatusName = (string)reader[BaseDao.LEAVE_STATUS_NAME];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Account), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
