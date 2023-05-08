using CompanyManagement.Database;
using CompanyManagement.Database.Base;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

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
        public int Month { get => month; set { month = value; OnPropertyChanged(); LoadSalaryRecords(); } }

        private int year = DateTime.Now.Year;
        public int Year { get => year; set { year = value; OnPropertyChanged(); LoadSalaryRecords(); } }

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
            LoadSalaryRecords();
        }

        public void SetCommands()
        {
            CalculateSalaryCommand = new RelayCommand<object>(ExcuteCalculateSalary);
            DistributeSalaryCommand = new RelayCommand<object>(ExcuteDistributeSalary);
            OpenSalaryDetailsDialogCommand = new RelayCommand<SalaryRecord>(ExcuteOpenSalaryDetailsDialog);
            RestorePayRollCommand = new RelayCommand<object>(ExcuteRestorePayRoll);
        }

        private void LoadSalaryRecords()
        {
            listSalaryRecord = (departmentID == "") 
                ? salaryRecordsDao.GetByTime(Month, Year) 
                : salaryRecordsDao.GetByDepartmentID(DepartmentID, Month, Year);
            foreach(SalaryRecord salaryRecord in listSalaryRecord)
            {
                salaryRecord.Worker = employeesDao.SearchByID(salaryRecord.EmployeeID);
                salaryRecord.TotalOffDays = GetToTalDayOfMonth() - salaryRecord.TotalWorkDays;
            }
            SalaryRecords = new ObservableCollection<SalaryRecord>(listSalaryRecord);
        }
            
        private void SetComboBox()
        {
            Departments = new List<Department>();
            Departments.Add(new Department("ALL", "Tất Cả", ""));
            Departments.AddRange(departmentsDao.GetAll());
            Years = Enumerable.Range(2000, DateTime.Now.Year - 1999).OrderByDescending(year => year).ToList();
        }

        private void ExcuteCalculateSalary(object obj)
        {
            if (!ValidateCalculateSalary()) return;
            var employees = employeesDao.GetAll();
            listSalaryRecord.Clear();
            foreach(Employee employee in employees)
            {
                SalaryRecord salaryRecord = CreateSalaryRecord(employee);
                salaryRecord.Worker = employee;
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
            int totalWorkDays = timeSheetsDao.SearchByEmployeeID(employee.ID).Count + CountWeekendDays();
            int totalOffDays = GetToTalDayOfMonth() - totalWorkDays;
            decimal totalBonuses = projectBonusesDao.ToTalBonusesOfEmployeeByTime(employee.ID, month, year);
            return new SalaryRecord("", employee.ID, monthYear, totalWorkDays, totalOffDays, totalBonuses, 0);
        }

        private void CalculateIcome(SalaryRecord salaryRecord)
        {
            salaryRecord.Income = (decimal)((salaryRecord.TotalWorkDays * salaryRecord.Worker.Salary)/ GetToTalDayOfMonth() + salaryRecord.TotalBonuses);
        }

        private int GetToTalDayOfMonth()
        {
            return DateTime.DaysInMonth(year,month);
        }

        private int CountWeekendDays()
        {
            int number = 0;
            int days = GetToTalDayOfMonth();
            for(int day = 1; day <= days; day++)
            {
                DateTime date = new DateTime(year, month, day);
                if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                    number++;
            }
            return number;
        }

        private void ExcuteDistributeSalary(object obj)
        {
            if (!ValidateDistributeSalary()) return;
            var dialog = new AlertDialogService(
               "Phát Lương",
               "Bạn chắc chắn muốn phát lương cho nhân viên?",
               () =>
               {
                   AddToDB();
               }, null);
            dialog.Show();
        }

        private void AddToDB()
        {
            foreach (SalaryRecord salaryRecord in SalaryRecords)
            {
                salaryRecord.ID = AutoGenerateID();
                salaryRecordsDao.Add(salaryRecord);
            }
            if (salaryRecordsDao.GetByTime(month, year).Count != 0)
            {
                RestoreButton();
            }
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
            VisibleRestoreButton = Visibility.Visible; 
            Timer = 15;
            StartTimer();
        }

        private async void StartTimer()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            Timer--;
            if (Timer > 0)
                StartTimer();
            else
                VisibleRestoreButton = Visibility.Hidden;
        }

        private void LoadMessageRemain()
        {
            MessageTimeRemain = "";
            if (timer > 0)
                MessageTimeRemain = "Còn " + timer + " giây để hoàn tác!";
        }

        private void ExcuteRestorePayRoll(object obj)
        {
            var dialog = new AlertDialogService(
               "Hoàn tác",
               "Bạn chắc chắn muốn hoàn tác việc phát lương cho nhân viên?",
               () =>
               {
                   DeleteFromDB();
               }, null);
            dialog.Show();
        }

        private void DeleteFromDB()
        {
            salaryRecordsDao.DeleteByMonthYear(month, year);
            LoadSalaryRecords();
        }

        private void ExcuteOpenSalaryDetailsDialog(SalaryRecord salaryRecord)
        {
            SalaryDetailsDialog SalaryDetailDiaLog = new SalaryDetailsDialog();
            SalaryDetailViewModel salaryDetailViewModel = new SalaryDetailViewModel();
            salaryDetailViewModel.salaryRecordIns = salaryRecord;
            SalaryDetailDiaLog.DataContext = salaryDetailViewModel;
            SalaryDetailDiaLog.Show();
        }
    }
}
