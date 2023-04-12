using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInViewModel : BaseViewModel
    {
        private CheckInOut checkInOut;

        public string ID { get => checkInOut.ID; set { checkInOut.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => checkInOut.EmployeeID; set { checkInOut.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime CheckInTime { get => checkInOut.CheckInTime; set { checkInOut.CheckInTime = value; OnPropertyChanged(); } }
        public string TaskID { get => checkInOut.TaskID; set { checkInOut.TaskID = value; OnPropertyChanged(); } }
    }
}
