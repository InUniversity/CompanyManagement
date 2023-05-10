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
        private DateTime created = DateTime.Now;
        private DateTime start = DateTime.Now;
        private DateTime end = DateTime.Now;
        private string statusID = "";
        private string requesterID = "";
        private string approverID = "";
        private Employee approver = new Employee();
        private LeaveStatus status = new LeaveStatus();

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

        public DateTime Created
        {
            get => created;
            set => created = value;
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

        public string StatusID
        {
            get => statusID;
            set => statusID = value;
        }

        public string RequesterID
        {
            get => requesterID;
            set => requesterID = value;
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

        public LeaveStatus Status
        {
            get => status;
            set => status = value;
        }

        public LeaveRequest() { }

        public LeaveRequest(string id, string reason, string notes, string statusID, string requesterID, string approverID)
        {
            this.id = id;
            this.reason = reason;
            this.notes = notes;
            this.statusID = statusID;
            this.requesterID = requesterID;
            this.approverID = approverID;
        }

        public LeaveRequest(IDataRecord reader)
        {
            try
            {
                id = Utils.GetString(reader, BaseDao.leavID);
                reason = Utils.GetString(reader, BaseDao.leavReason);
                notes = Utils.GetString(reader, BaseDao.leavNotes);
                created = Utils.GetDateTime(reader, BaseDao.leavCreated);
                start = Utils.GetDateTime(reader, BaseDao.leavStart);
                end = Utils.GetDateTime(reader, BaseDao.leavEnd);
                statusID = Utils.GetString(reader, BaseDao.leavStatusID);
                requesterID = Utils.GetString(reader, BaseDao.leavEmplID);
                approverID = Utils.GetString(reader, BaseDao.leavApproverID);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
