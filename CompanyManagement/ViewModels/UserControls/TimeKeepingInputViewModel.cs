using System;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface ITimeKeepingInput
    {
        TimeKeeping CreateTimeKeepingInstance();
        void TrimAllTexts();
        void Retrieve(TimeKeeping timeKeeping);
    }

    public class TimeKeepingInputViewModel: BaseViewModel, ITimeKeepingInput
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

        public TimeKeeping CreateTimeKeepingInstance()
        {
            return new TimeKeeping(taskID, start, end, employeeID, notes, createBy);
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
            TaskID = timeKeeping.TaskID;
            Start = timeKeeping.StartTime;
            End = timeKeeping.EndTime;
            EmployeeID = timeKeeping.EmployeeID;
            Notes = timeKeeping.Notes;
            CreateBy = timeKeeping.CreateBy;
        }
    }
}
