using System;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;


namespace CompanyManagement.ViewModels.UserControls
{

    public interface ITimeKeepingInput
    {
        string TaskID { get; }
        string EmployeeID { get; }
        string CreateBy { get; }
        string ErrorMessage { set; }
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

        public TimeKeepingInputViewModel() { }

        public TimeKeeping CreateTimeKeepingInstance()
        {
            return new TimeKeeping(taskID, Utils.TimeToString(start), Utils.TimeToString(end), employeeID, notes, createBy);
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
}
