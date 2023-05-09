using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace CompanyManagement.ViewModels.UserControls
{
    public class SalaryDetailViewModel : BaseViewModel
    {
        public SalaryRecord salaryRecordIns = new SalaryRecord();
        public string ID { get => salaryRecordIns.ID; set { salaryRecordIns.ID = value; OnPropertyChanged(); } }
        public string EmployeeID { get => salaryRecordIns.EmployeeID; set { salaryRecordIns.EmployeeID = value; OnPropertyChanged(); } }
        public DateTime MonthYear { get => salaryRecordIns.MonthYear; set { salaryRecordIns.MonthYear = value; OnPropertyChanged(); } }
        public int TotalWorkDays { get => salaryRecordIns.TotalWorkDays; set { salaryRecordIns.TotalWorkDays = value; OnPropertyChanged(); } }
        public int TotalOffDays { get => salaryRecordIns.TotalOffDays; set { salaryRecordIns.TotalOffDays = value; OnPropertyChanged(); } }
        public decimal TotalBonuses { get => salaryRecordIns.TotalBonuses; set { salaryRecordIns.TotalBonuses = value; OnPropertyChanged(); } }
        public decimal Income { get => salaryRecordIns.Income; set { salaryRecordIns.Income = value; OnPropertyChanged(); } }
        public Employee Worker { get => salaryRecordIns.Worker; set { salaryRecordIns.Worker = value; OnPropertyChanged(); } }
        public Role WorkerRole { get => salaryRecordIns.WorkerRole; }
        public Department WorkerDepartment { get => salaryRecordIns.WorkerDepartment; }

        //    public List<DateTime> OffDays = new List<DateTime>();

        public List<ProjectBonus> ProjectsCompleted = new List<ProjectBonus>();

        private ProjectBonusesDao projectBonusesDao = new ProjectBonusesDao();
        
        public ICommand CloseViewCommand { get; private set; }

        public SalaryDetailViewModel()
        {
            SetList();
            SetCommad();
        }

        public void SetList()
        {
            ProjectsCompleted = projectBonusesDao.GetOfEmployeeByTime(EmployeeID, MonthYear.Month, MonthYear.Year);
        }

        public void SetCommad()
        {
            CloseViewCommand = new RelayCommand<Window>(ExcuteCloseView);
        }

        private void ExcuteCloseView(Window viewWindow)
        {
            viewWindow.Close();
        }
    }
}
