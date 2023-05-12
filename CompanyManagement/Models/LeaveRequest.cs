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
        private string response = "";
        private Employee approver = new Employee();
        private Employee requester = new Employee();
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

        public string Response
        {
            get => response;
            set => response = value;
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

        public Employee Requester
        {
            get => requester;
            set => requester = value;
        }

        public LeaveStatus Status
        {
            get => status;
            set => status = value;
        }

        public LeaveRequest() { }

        public LeaveRequest(string id, string reason, string notes, string statusID, 
            string requesterID, string approverID, string response)
        {
            this.id = id;
            this.reason = reason;
            this.notes = notes;
            this.statusID = statusID;
            this.requesterID = requesterID;
            this.approverID = approverID;
            this.response = response;
        }

        public LeaveRequest(IDataRecord record)
        {
            try
            {
                id = Utils.GetString(record, BaseDao.leavID);
                reason = Utils.GetString(record, BaseDao.leavReason);
                notes = Utils.GetString(record, BaseDao.leavNotes);
                created = Utils.GetDateTime(record, BaseDao.leavCreated);
                start = Utils.GetDateTime(record, BaseDao.leavStart);
                end = Utils.GetDateTime(record, BaseDao.leavEnd);
                statusID = Utils.GetString(record, BaseDao.leavStatusID);
                requesterID = Utils.GetString(record, BaseDao.leavEmplID);
                approverID = Utils.GetString(record, BaseDao.leavApproverID);
                response = Utils.GetString(record, BaseDao.leavResponse);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(Employee), "CAST ERROR: " + ex.Message);
            }
        }
    }
}
