using CompanyManagement.Database.Base;
using CompanyManagement.Utilities;
using System;
using System.Data;
using CompanyManagement.Database;
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
        private Employee approver = new Employee();

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

        public Employee Approver
        {
            get => approver;
            set => approver = value;
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
                id = Utils.GetString(reader, BaseDao.LEAVES_ID);
                reason = Utils.GetString(reader, BaseDao.LEAVES_REASON);
                notes = Utils.GetString(reader, BaseDao.LEAVES_NOTES);
                createdDate = Utils.GetDateTime(reader, BaseDao.LEAVES_CREATED_DATE);
                startDate = Utils.GetDateTime(reader, BaseDao.LEAVES_START_DATE);
                endDate = Utils.GetDateTime(reader, BaseDao.LEAVES_END_DATE);
                statusID = Utils.GetString(reader, BaseDao.LEAVES_STATUS_ID);
                employeeID = Utils.GetString(reader, BaseDao.LEAVES_EMPLOYEE_ID);
                approverID = Utils.GetString(reader, BaseDao.LEAVES_APPROVER_ID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
