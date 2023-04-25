using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Net;
using System.Xml.Linq;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInOutInputViewModel : BaseViewModel
    {
        private CheckInOut checkInOut;
        public string ID { get => checkInOut.ID; set { checkInOut.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => checkInOut.EmployeeID; set { checkInOut.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime CheckInTime { get => checkInOut.CheckInTime; set { checkInOut.CheckInTime = value; OnPropertyChanged(); } }
        public DateTime CheckOutTime { get => checkInOut.CheckOutTime; set { checkInOut.CheckOutTime = value; OnPropertyChanged(); } }
        public string TaskCheckInID { get => checkInOut.TaskCheckInID; set { checkInOut.TaskCheckInID = value; OnPropertyChanged(); } }

        public CheckInOutInputViewModel()
        {
            checkInOut = new CheckInOut();
        }

        public CheckInOut CreateCheckInOut()
        {
            return checkInOut;
        }

        public void ReceiveCheckInOut(CheckInOut checkInOut)
        {
            this.checkInOut = checkInOut;
        }
    }
}
