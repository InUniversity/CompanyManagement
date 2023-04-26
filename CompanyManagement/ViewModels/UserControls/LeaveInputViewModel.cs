using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ILeaveInput
    {
        string ID { get; set; }
        string EmployeeID { get; set; }
        string LeaveTypeID { get; set; }
        string LeaveReason { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        string LeaveStatusID { get; set; }
        DateTime CreateDate { get; set; }
        string ApprovedBy { get; set; }
        string Note { get; set; }
        string ErrorMessage { get; set; }
        bool IsReadOnly { get; set; }
        List<LeaveType> LeaveTypes { get; set; }
        List<LeaveStatus> LeaveStatuses { get; set; }
        void TrimAllTexts();
        public Leave LeaveIns { get; set; }
    }

    public class LeaveInputViewModel : BaseViewModel, ILeaveInput
    {
        private Leave leave;
        public Leave LeaveIns { get => leave; set => leave = value; }

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

        private bool isReadOnly = false;
        public bool IsReadOnly { get => isReadOnly; set { isReadOnly = value; OnPropertyChanged(); } }

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
    }
}
