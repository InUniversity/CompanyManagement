using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class SalaryRecordsViewModel : BaseViewModel
    {
        private ObservableCollection<SalaryRecord> salaryRecords = new ObservableCollection<SalaryRecord>();
        public ObservableCollection<SalaryRecord> SalaryRecords { get => salaryRecords; set { salaryRecords = value; OnPropertyChanged(); }  }

        private List<SalaryRecord> listSalaryRecord = new List<SalaryRecord>();
        public List<int> Years { get; private set; }
        public List<Department> Departments { get; private set; }

        private int month = DateTime.Now.Month - 1;
        public int Month { get => month; set { month = value; OnPropertyChanged(); LoadSalaryRecords(); LoadToTalDay(); } }

        private int year = DateTime.Now.Year;
        public int Year { get => year; set { year = value; OnPropertyChanged(); LoadSalaryRecords(); LoadToTalDay(); } }

        private int totalDayByTime = 0;
        private int totalDayWeekend = 0;

        private string departmentID = "ALL";
        public string DepartmentID { get => departmentID; set { departmentID = value; OnPropertyChanged(); LoadSalaryRecords(); } }

        private Visibility visibleRestoreButton = Visibility.Hidden;
        public Visibility VisibleRestoreButton { get => visibleRestoreButton; set { visibleRestoreButton = value; OnPropertyChanged(); } }

        private int timer = 0;
        public int Timer { get => timer; set { timer = value; OnPropertyChanged(); LoadMessageRemain(); } }

        public string messageTimeRemain = "";
        public string MessageTimeRemain { get => messageTimeRemain; set { messageTimeRemain = value; OnPropertyChanged(); } }

        private SalaryRecordsDao salaryRecordsDao = new SalaryRecordsDao();
        private DepartmentsDao departmentsDao = new DepartmentsDao();
        private RolesDao rolesDao = new RolesDao();
        private EmployeesDao employeesDao = new EmployeesDao();
        private TimeSheetsDao timeSheetsDao = new TimeSheetsDao();
        private ProjectBonusesDao projectBonusesDao = new ProjectBonusesDao();

        public ICommand CalculateSalaryCommand { get; private set; }
        public ICommand DistributeSalaryCommand { get; private set; }
        public ICommand OpenSalaryDetailsDialogCommand { get; private set; }
        public ICommand RestorePayRollCommand { get; private set; }

        public SalaryRecordsViewModel()
        {
            SetComboBox();
            SetCommands();
            LoadToTalDay();
            LoadSalaryRecords(); 
        }

        private void SetCommands()
        {
            CalculateSalaryCommand = new RelayCommand<object>(ExecuteCalculateSalary);
            DistributeSalaryCommand = new RelayCommand<object>(ExecuteDistributeSalary);
            OpenSalaryDetailsDialogCommand = new RelayCommand<SalaryRecord>(ExecuteOpenSalaryDetailsDialog);
            RestorePayRollCommand = new RelayCommand<object>(ExecuteRestorePayRoll);
        }

        private void LoadSalaryRecords()
        {
            listSalaryRecord = (departmentID == "") 
                ? salaryRecordsDao.GetByTime(Month, Year) 
                : salaryRecordsDao.GetByDepartmentID(DepartmentID, Month, Year);
            SalaryRecords = new ObservableCollection<SalaryRecord>(listSalaryRecord);
        }
            
        private void SetComboBox()
        {
            Departments = new List<Department>();
            Departments.Add(new Department("ALL", "Tất Cả", ""));
            Departments.Add(new Department("MNG", "Management", ""));
            Departments.AddRange(departmentsDao.GetAll());
            Years = Enumerable.Range(2000, DateTime.Now.Year - 1999).OrderByDescending(year => year).ToList();
        }

        private void ExecuteCalculateSalary(object obj)
        {
            if (!ValidateCalculateSalary()) return;
            var employees = employeesDao.GetAll();
            listSalaryRecord.Clear();
            foreach(Employee employee in employees)
            {
                SalaryRecord salaryRecord = CreateSalaryRecord(employee);
                CalculateIcome(salaryRecord);
                listSalaryRecord.Add(salaryRecord);
            }
            SalaryRecords = new ObservableCollection<SalaryRecord>(listSalaryRecord);
        }

        private bool ValidateCalculateSalary()
        {
            if (DateTime.Now.Year == Year && DateTime.Now.Month <= Month)
            {
                var dialog = new AlertDialogService(
                    "Thông báo",
                    "Chưa đến ngày tính lương!", null, null);
                dialog.Show();
                return false;
            }

            if (SalaryRecords.Count != 0)
            {
                var dialog = new AlertDialogService(
                    "Thông báo",
                    "Lương đã được tính trong quá khứ!", null, null);
                dialog.Show();
                return false;
            }
            return true;
        }

        private SalaryRecord CreateSalaryRecord(Employee employee)
        {
            DateTime monthYear = new DateTime(year, month, 1);
            int totalWorkDays = timeSheetsDao.ToTalWorksDayByEmployeeID(employee.ID) + totalDayWeekend;
            decimal totalBonuses = projectBonusesDao.ToTalBonusesOfEmployeeByTime(employee.ID, month, year);
            return new SalaryRecord("", employee.ID, monthYear, totalWorkDays, totalBonuses, 0);
        }

        private void CalculateIcome(SalaryRecord salaryRecord)
        {
            salaryRecord.Income = (salaryRecord.TotalWorkDays * salaryRecord.WorkerRole.Salary)/ totalDayByTime + salaryRecord.TotalBonuses;
        }

        private void LoadToTalDay()
        {
            LoadToTalDayOfMonth();
            CountWeekendDays();
        }    

        private void LoadToTalDayOfMonth()
        {
            totalDayByTime = DateTime.DaysInMonth(year,month);
        }

        private void CountWeekendDays()
        {
            totalDayWeekend = 0;
            for(int day = 1; day <= totalDayByTime; day++)
            {
                DateTime date = new DateTime(year, month, day);
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                    totalDayWeekend++;
            }
        }

        private void ExecuteDistributeSalary(object obj)
        {
            if (!ValidateDistributeSalary()) return;
            var dialog = new AlertDialogService(
               "Phát Lương",
               "Bạn chắc chắn muốn phát lương cho nhân viên?",
               () =>
               {
                   AddToDatabase();
               }, null);
            dialog.Show();
        }

        private void AddToDatabase()
        {
            foreach (SalaryRecord salaryRecord in SalaryRecords)
            {
                salaryRecord.ID = AutoGenerateID();
                salaryRecordsDao.Add(salaryRecord);
            }
            if (salaryRecordsDao.GetByTime(month, year).Count != 0)
                RestoreButton();
        }

        private string AutoGenerateID()
        {
            string salaryRecordID;
            Random random = new Random();
            do
            {
                int number = random.Next(100000);
                salaryRecordID = $"SR{number:00000}";
            } while (salaryRecordsDao.SearchByID(salaryRecordID) != null);
            return salaryRecordID;
        }

        private bool ValidateDistributeSalary()
        {
            if (salaryRecordsDao.GetByTime(month, year).Count != 0)
            {
                var dialog = new AlertDialogService(
                    "Thông báo",
                    "Tháng này đã được thanh toán trong quá khứ", null, null);
                dialog.Show();
                return false;
            }
            if (SalaryRecords.Count == 0)
            {
                var dialog = new AlertDialogService(
                    "Thông báo",
                    "Bạn chưa tính lương cho nhân viên", null, null);
                dialog.Show();
                return false;
            }
            return true;
        }

        private void RestoreButton()
        {
            ShowRestoreButton();
            StartTimer();
        }

        private void ShowRestoreButton()
        {
            VisibleRestoreButton = Visibility.Visible;
            Timer = 15;
        }

        private void HideRestoreButton()
        {
            VisibleRestoreButton = Visibility.Hidden;
            Timer = 0;
        }

        private async void StartTimer()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            Timer--;
            if (Timer > 0)
                StartTimer();
            else
                HideRestoreButton();
        }

        private void LoadMessageRemain()
        {
            MessageTimeRemain = "";
            if (timer > 0)
                MessageTimeRemain = "Còn " + timer + " giây để hoàn tác!";
        }

        private void ExecuteRestorePayRoll(object obj)
        {
            var dialog = new AlertDialogService(
               "Hoàn tác",
               "Bạn chắc chắn muốn hoàn tác việc phát lương cho nhân viên?",
               () =>
               {
                   DeleteFromDatabase();
               }, null);
            dialog.Show();
        }

        private void DeleteFromDatabase()
        {
            salaryRecordsDao.DeleteByMonthYear(month, year);
            LoadSalaryRecords();
            HideRestoreButton();
        }

        private void ExecuteOpenSalaryDetailsDialog(SalaryRecord salaryRecord)
        {
            SalaryDetailsDialog salaryDetailDiaLog = new SalaryDetailsDialog();
            SalaryDetailViewModel salaryDetailViewModel = new SalaryDetailViewModel();
            salaryDetailViewModel.salaryRecordIns = salaryRecord;
            salaryDetailDiaLog.DataContext = salaryDetailViewModel;
            salaryDetailDiaLog.Show();
        }
    }
}
