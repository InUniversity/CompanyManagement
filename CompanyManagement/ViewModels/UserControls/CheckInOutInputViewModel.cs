﻿using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInOutInputViewModel : BaseViewModel
    {
        private CheckInOut checkInOut;

        public string ID { get => checkInOut.ID; set { checkInOut.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => checkInOut.EmployeeID; set { checkInOut.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime CheckInTime { get => checkInOut.CheckInTime; set { checkInOut.CheckInTime = value; OnPropertyChanged(); } }
        public DateTime CheckOutTime { get => checkInOut.CheckOutTime; set { checkInOut.CheckOutTime = value; OnPropertyChanged(); } }
        public bool CheckOutStatus { get => checkInOut.CheckOutStatus; set { checkInOut.CheckOutStatus = value; OnPropertyChanged(); } }
        public string TaskID { get => checkInOut.TaskID; set { checkInOut.TaskID = value; OnPropertyChanged(); } }
        public string CompletedTaskID { get => checkInOut.CompletedTaskID; set { checkInOut.CompletedTaskID = value; OnPropertyChanged(); } }

        public CheckInOutInputViewModel()
        {
            checkInOut = new CheckInOut();
        }
    }
}