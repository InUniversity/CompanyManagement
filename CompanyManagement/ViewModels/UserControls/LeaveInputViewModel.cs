using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;

namespace CompanyManagement.ViewModels.UserControls
{
    public class LeaveInputViewModel : BaseViewModel
    {
        private LeaveRequest leaveRequest;
        public LeaveRequest LeaveRequestIns { get => leaveRequest; set => leaveRequest = value; }

        public string ID { get => leaveRequest.ID; set { leaveRequest.ID = value; OnPropertyChanged(); } } 
        public string Reason { get => leaveRequest.Reason; set { leaveRequest.Reason = value; OnPropertyChanged(); } }
        public string Notes { get => leaveRequest.Notes; set { leaveRequest.Notes = value; OnPropertyChanged(); } }
        public DateTime CreatedDate { get => leaveRequest.CreatedDate; set { leaveRequest.CreatedDate = value; OnPropertyChanged(); } }
        public DateTime StartDate { get => leaveRequest.StartDate; set { leaveRequest.StartDate = value; OnPropertyChanged(); } }
        public DateTime EndDate { get => leaveRequest.EndDate; set { leaveRequest.EndDate = value; OnPropertyChanged(); } }
        public string StatusID { get => leaveRequest.StatusID; set { leaveRequest.StatusID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => leaveRequest.EmployeeID; set { leaveRequest.EmployeeID = value; OnPropertyChanged(); } }
        public string ApproverID { get => leaveRequest.ApproverID; set { leaveRequest.ApproverID = value; OnPropertyChanged(); } }
        
        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private bool isReadOnly = false;
        public bool IsReadOnly { get => isReadOnly; set { isReadOnly = value; OnPropertyChanged(); } }

        public List<LeaveStatus> LeaveStatuses { get; set;}

        private LeaveStatusesDao leaveStatusesDao = new LeaveStatusesDao();

        public LeaveInputViewModel()
        {
            SetAllComboBox();
        }

        private void SetAllComboBox()
        {
            LeaveStatuses = leaveStatusesDao.GetAll();
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            Reason = Reason.Trim();
            Notes = Notes.Trim();
            EmployeeID = EmployeeID.Trim();
            ApproverID = ApproverID.Trim();
        }
    }
}
