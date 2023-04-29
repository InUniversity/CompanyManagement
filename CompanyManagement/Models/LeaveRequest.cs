using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;
using System.Windows.Media;

namespace CompanyManagement.Models
{
    public class LeaveRequest
    {
        private string id = "";
        private string reason = "";
        private string notes = "";
        private DateTime createdDate = DateTime.Now;
        private DateTime startDate = DateTime.Now;
        private DateTime endDate = DateTime.Now;
        private string statusID = "";
        private string employeeID = "";
        private string approverID = "";

        public string ID
        {
            get => id;
            set => id = value;
        }

        public string Reason
        {
            get => reason;
            set => reason = value;
        }

        public string Notes
        {
            get => notes;
            set => notes = value;
        }

        public DateTime CreatedDate
        {
            get => createdDate;
            set => createdDate = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
        }

        public string StatusID
        {
            get => statusID;
            set => statusID = value;
        }

        public string EmployeeID
        {
            get => employeeID;
            set => employeeID = value;
        }

        public string ApproverID
        {
            get => approverID;
            set
            { 
                if (value != null) 
                {  
                    approverID = value;
                    return;
                }
                approverID = "";
            }
        }

        public LeaveRequest() { }

        public LeaveRequest(string id, string reason, string notes, string statusID, string employeeID, string approverID)
        {
            this.id = id;
            this.reason = reason;
            this.notes = notes;
            this.statusID = statusID;
            this.employeeID = employeeID;
            this.approverID = approverID;
        }

        public LeaveRequest(IDataRecord reader)
        {
            try
            {
                id = (string)reader[BaseDao.LEAVE_ID];
                reason = (string)reader[BaseDao.LEAVE_REASON];
                notes = (string)reader[BaseDao.LEAVE_NOTES];
                createdDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.LEAVE_CREATED_DATE));
                startDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.LEAVE_START_DATE));
                endDate = reader.GetDateTime(reader.GetOrdinal(BaseDao.LEAVE_END_DATE));
                statusID = (string)reader[BaseDao.LEAVE_STATUS_ID];
                employeeID = (string)reader[BaseDao.LEAVE_EMPLOYEE_ID];
                approverID = (string)reader[BaseDao.LEAVE_APPROVER_ID];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
