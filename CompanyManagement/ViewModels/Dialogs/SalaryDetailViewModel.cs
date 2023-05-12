using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class SalaryDetailViewModel : BaseViewModel
    {
        public SalaryRecord salaryRecordIns = new SalaryRecord();
        public string ID { get => salaryRecordIns.ID; set { salaryRecordIns.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => salaryRecordIns.EmployeeID; set { salaryRecordIns.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime MonthYear { get => salaryRecordIns.MonthYear; set { salaryRecordIns.MonthYear = value; OnPropertyChanged(); } }
        public int TotalWorkDays { get => salaryRecordIns.TotalWorkDays; set { salaryRecordIns.TotalWorkDays = value; OnPropertyChanged(); } }
        public int TotalOffDays { get => salaryRecordIns.TotalOffDays; }
        public decimal TotalBonuses { get => salaryRecordIns.TotalBonuses; set { salaryRecordIns.TotalBonuses = value; OnPropertyChanged(); } }
        public decimal Income { get => salaryRecordIns.Income; set { salaryRecordIns.Income = value; OnPropertyChanged(); } }
        public Employee Worker { get => salaryRecordIns.Worker; }
        public Role WorkerRole { get => salaryRecordIns.WorkerRole; }
        public Department WorkerDepartment { get => salaryRecordIns.WorkerDepartment; }

        private List<LeaveRequest> leaveRequests;
        public List<LeaveRequest> LeaveRequests { get => leaveRequests; set { leaveRequests = value; OnPropertyChanged(); } }

        public List<ProjectBonus> ProjectsCompleted = new List<ProjectBonus>();

        private ProjectBonusesDao projectBonusesDao = new ProjectBonusesDao();
        private LeaveRequestsDao leaveRequestsDao = new LeaveRequestsDao();

        public SalaryDetailViewModel()
        {
            LoadCompletedProject();
            LoadLeaveRequests();
        }

        private void LoadCompletedProject()
        {
            ProjectsCompleted = projectBonusesDao.GetOfEmployeeByTime(EmployeeID, MonthYear.Month, MonthYear.Year);
        }

        private void LoadLeaveRequests()
        {
            var listRequests = leaveRequestsDao.GetMyRequests(EmployeeID);
            var listItem = from request in listRequests /*where request.Start.Month == MonthYear.Month*/  select request;
            LeaveRequests = listItem.ToList();
        }
    }
}
