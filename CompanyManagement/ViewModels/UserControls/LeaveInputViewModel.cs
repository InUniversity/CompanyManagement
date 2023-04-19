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
        void Receive(Leave leave);
    }

    public class LeaveInputViewModel: BaseViewModel, ILeaveInput
    {

        private Leave leave;

        public string ID { get => leave.ID; set { leave.ID = value; OnPropertyChanged(); } } 

        public string EmployeeID { get => leave.EmployeeID; set { leave.EmployeeID = value; OnPropertyChanged(); } }

        public string LeaveTypeID { get => leave.LeaveTypeID; set { leave.LeaveTypeID = value; OnPropertyChanged(); } }

        public string LeaveReason { get => leave.LeaveReason; set { leave.LeaveReason = value; OnPropertyChanged(); } }

        public DateTime Start { get => leave.Start; set { leave.Start = value; OnPropertyChanged(); } }

        public DateTime End { get => leave.End; set { leave.End = value; OnPropertyChanged(); } }

        public string LeaveStatusID { get => leave.LeaveStatusID; set { leave.LeaveStatusID = value; OnPropertyChanged(); } }

        public DateTime CreateDate { get => leave.CreateDate; set { leave.CreateDate = value; OnPropertyChanged(); } }

        public string ApprovedBy { get => leave.ApprovedBy; set { leave.ApprovedBy = value; OnPropertyChanged(); } }

        public string Note { get => leave.Note; set { leave.Note = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        public List<LeaveType> LeaveTypes { get; set; }
        public List<LeaveStatus> LeaveStatuses { get; set;}

        private LeaveTypeDao leaveTypeDao = new LeaveTypeDao();
        private LeaveStatusDao leaveStatusDao = new LeaveStatusDao();

        public LeaveInputViewModel()
        {
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            LeaveTypes = leaveTypeDao.GetAll();
            LeaveStatuses = leaveStatusDao.GetAll();
        }

        public Leave CreateLeaveInstance()
        {
            return new Leave(ID, EmployeeID , LeaveTypeID, LeaveReason, 
                Start, End, LeaveStatusID, CreateDate, ApprovedBy, Note);
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            EmployeeID = EmployeeID.Trim();
            LeaveTypeID = LeaveTypeID.Trim();
            LeaveReason = LeaveReason.Trim();
            LeaveStatusID = LeaveStatusID.Trim();
            ApprovedBy = ApprovedBy.Trim();
            Note = Note.Trim();
        }

        public void Receive(Leave leave)
        {
            this.leave = leave;
        }
    }
}
