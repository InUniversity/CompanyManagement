using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ILeaveInput
    {
        string ID { get; }
        string EmployeeID { get; }
        string LeaveReason { get; }
        DateTime Start { get; }
        DateTime End { get; }
        DateTime CreateDate { get; }
        string ApprovedBy { get; }
        string Note { get; }
        string ErrorMessage { set; }
        Leave CreateLeaveInstance();
        void TrimAllTexts();
        void Retrieve(Leave leave);
    }

    public class LeaveInputViewModel: BaseViewModel, ILeaveInput
    {
        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string employeeID = "";
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private string leaveTypeID = "";
        public string LeaveTypeID { get => leaveTypeID; set { leaveTypeID = value; OnPropertyChanged(); } }

        private string leaveReason = "";
        public string LeaveReason { get => leaveReason; set { leaveReason = value; OnPropertyChanged(); } }

        private DateTime start = DateTime.Now;
        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }

        private DateTime end = DateTime.Now;
        public DateTime End { get => end; set { end = value; OnPropertyChanged(); } }

        private string leaveStatusID = "LS2";
        public string LeaveStatusID { get => leaveStatusID; set { leaveStatusID = value; OnPropertyChanged(); } }

        private DateTime createDate = DateTime.Now;
        public DateTime CreateDate { get => createDate; set { createDate = value; OnPropertyChanged(); } }

        private string approvedBy = "";
        public string ApprovedBy { get => approvedBy; set { approvedBy = value; OnPropertyChanged(); } }

        private string note = "";
        public string Note { get => note; set { note = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }  

        public List<LeaveType> LeaveTypes { get; set; }
        public List<LeaveStatus> LeaveStatuses { get; set;}

        private LeaveTypeDao leaveTypeDao;
        private LeaveStatusDao leaveStatusDao;

        public LeaveInputViewModel()
        {
            leaveTypeDao = new LeaveTypeDao();
            leaveStatusDao = new LeaveStatusDao();
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            LeaveTypes = leaveTypeDao.GetAll();
            LeaveStatuses = leaveStatusDao.GetAll();
        }

        public Leave CreateLeaveInstance()
        {
            return new Leave(id, employeeID, leaveTypeID, leaveReason, start, end, LeaveStatusID, createDate, approvedBy, note);
        }

        public void TrimAllTexts()
        {
            id = id.Trim();
            employeeID = employeeID.Trim();
            leaveTypeID = leaveTypeID.Trim();
            leaveReason = leaveReason.Trim();
            leaveStatusID = leaveStatusID.Trim();
            approvedBy = approvedBy.Trim();
            note = note.Trim();
        }

        public void Retrieve(Leave leave)
        {
            ID = leave.ID;
            employeeID = leave.EmployeeID;
            leaveTypeID = leave.LeaveTypeID;
            LeaveReason = leave.LeaveReason;
            Start = leave.Start;
            End = leave.End;
            LeaveStatusID = leave.LeaveStatusID;
            ApprovedBy = leave.ApprovedBy;
            Note = leave.Note;
        }
    }
}
