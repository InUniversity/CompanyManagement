using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.Views.UserControls;


namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeKeepingInputViewModel: BaseViewModel
    {
        private string taskID = "";
        public string TaskID { get => taskID; set { taskID = value; OnPropertyChanged(); } }

        private DateTime start = DateTime.Now;
        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }

        private DateTime end = DateTime.Now;
        public DateTime End { get => end; set { end = value; OnPropertyChanged(); } }

        private string employeeID = "";
        public string EmployeeID { get => employeeID; set { employeeID = value; OnPropertyChanged(); } }

        private string notes = "";
        public string Notes { get => notes; set { notes = value; OnPropertyChanged(); } }

        private string createBy = "";
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        public TimeKeepingInputViewModel() { }


        public TimeKeeping CreateTimeKeepingInstance()
        {
            return new TimeKeeping(taskID, Utils.DateTimeToString(start), Utils.DateTimeToString(end), employeeID, notes, createBy);
        }

        public void TrimAllTexts()
        {
            taskID = taskID.Trim();
            employeeID = employeeID.Trim();
            notes = notes.Trim();
            createBy = createBy.Trim();
        }

        public void Retrieve(TimeKeeping timeKeeping)
        {
            taskID = timeKeeping.TaskID;
            employeeID = timeKeeping.EmployeeID;
            notes = timeKeeping.Notes;
            createBy = timeKeeping.CreateBy;
        }
    }

    public interface IRetrieveTimeKeeping
    {
        void Retrieve(TimeKeeping timeKeeping);
    }
}
