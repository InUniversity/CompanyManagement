using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeSheetInputViewModel : BaseViewModel
    {
        private TimeSheet timeSheet;
        public TimeSheet TimeSheetIns { get => timeSheet; set => timeSheet = value; }

        public string ID { get => timeSheet.ID; set { timeSheet.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => timeSheet.EmployeeID; set { timeSheet.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime CheckInTime { get => timeSheet.CheckInTime; set { timeSheet.CheckInTime = value; OnPropertyChanged(); } }
        public DateTime CheckOutTime { get => timeSheet.CheckOutTime; set { timeSheet.CheckOutTime = value; OnPropertyChanged(); } }
        public string TaskCheckInID { get => timeSheet.TaskCheckInID; set { timeSheet.TaskCheckInID = value; OnPropertyChanged(); } }

        public TimeSheetInputViewModel()
        {
            timeSheet = new TimeSheet();
        }
    }
}
