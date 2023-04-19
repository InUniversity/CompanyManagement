using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data.SqlClient;

namespace CompanyManagement.Models
{
    public class Leave
    {
        private string id = "";
        private string employeeID = "";
        private string leaveTypeID = "";
        private string leaveReason = "";
        private DateTime start = DateTime.Now;
        private DateTime end = DateTime.Now;
        private string leaveStatusID = "";
        private DateTime createDate = DateTime.Now;
        private string approvedBy = "";
        private string note = "";

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

        public string LeaveTypeID
        {
            get => leaveTypeID;
            set => leaveTypeID = value;
        }

        public string LeaveReason
        {
            get => leaveReason;
            set => leaveReason = value;
        }

        public DateTime Start
        {
            get => start;
            set => start = value;
        }

        public DateTime End
        {
            get => end;
            set => end = value;
        }

        public string LeaveStatusID
        {
            get => leaveStatusID;
            set => leaveStatusID = value;
        }

        public DateTime CreateDate
        {
            get => createDate;
            set => createDate = value;
        }

        public string ApprovedBy
        {
            get => approvedBy;
            set => approvedBy = value;
        }

        public string Note
        {
            get => note;
            set => note = value;
        }

        public Leave() { }

        public Leave(string id, string employeeID, string leaveTypeID, string leaveReason, DateTime start, DateTime end, string leaveStatusID, DateTime createDate, string approvedBy, string note)
        {
            this.id = id;
            this.employeeID = employeeID;
            this.leaveTypeID = leaveTypeID;
            this.leaveReason = leaveReason;
            this.start = start;
            this.end = end;
            this.leaveStatusID = leaveStatusID;
            this.createDate = createDate;
            this.approvedBy = approvedBy;
            this.note = note;
        }

        public Leave(SqlDataReader reader)
        {
            try
            {
                id = (string)reader[BaseDao.LEAVE_ID];
                employeeID = (string)reader[BaseDao.LEAVE_EMPLOYEE_ID];
                leaveTypeID = (string)reader[BaseDao.LEAVE_TYPE_ID];
                leaveReason = (string)reader[BaseDao.LEAVE_REASON];
                start = reader.GetDateTime(reader.GetOrdinal(BaseDao.LEAVE_START_DATE));
                end = reader.GetDateTime(reader.GetOrdinal(BaseDao.LEAVE_END_DATE));
                leaveStatusID = (string)reader[BaseDao.LEAVE_STATUS_ID];
                approvedBy = (string)reader[BaseDao.LEAVE_APPROVED_BY];
                note = (string)reader[BaseDao.LEAVE_NOTES];
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
