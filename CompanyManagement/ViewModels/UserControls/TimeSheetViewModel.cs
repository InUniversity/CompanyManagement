using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeSheetViewModel : BaseViewModel
    {
        private bool isToggled;
        public bool IsToggled { get => isToggled; set { isToggled = value; OnPropertyChanged(); } }

        private TimeSheet currentTimeSheet;
        
        private ObservableCollection<TimeSheet> timeSheetList;
        public ObservableCollection<TimeSheet> TimeSheetList { get => timeSheetList; set { timeSheetList = value; } }

        private DateTime checkInTime;
        public DateTime CheckInTime { get => checkInTime; set { checkInTime = value; OnPropertyChanged(); } }
        
        private DateTime checkOutTime;
        public DateTime CheckOutTime { get => checkOutTime; set { checkOutTime = value; OnPropertyChanged(); } }

        public ICommand ToggledCommand { get; private set; }

        private TimeSheetsDao timeSheetsDao = new TimeSheetsDao();
        private TaskCheckOutsDao taskCheckOutsDao = new TaskCheckOutsDao();
        private TasksDao tasksDao = new TasksDao();

        public TimeSheetViewModel()
        {
            LoadTimeSheetList();
            SetCommands();
        }

        private void LoadTimeSheetList()
        {
            //TODO
            TimeSheetList = new ObservableCollection<TimeSheet>(timeSheetsDao.GetAll());
        }

        private void SetCommands()
        {
            ToggledCommand = new RelayCommand<object>(Toggled);
        }

        private void Toggled(object obj)
        {
            if (IsToggled) OpenCheckInDialog();
            else OpenCheckOutDialog();
            if (!string.IsNullOrWhiteSpace(currentTimeSheet.TaskCheckInID))
                IsToggled = !IsToggled;
        }

        private void OpenCheckInDialog()
        {
            CreateCheckIn();
            var inputService = new InputDialogService<TimeSheet>(new CheckInDialog(), currentTimeSheet, Add);
            inputService.Show();
        }

        private void Add(TimeSheet timeSheet)
        {
            CheckInTime = DateTime.Now;
            CheckOutTime = new DateTime();
            timeSheetsDao.Add(timeSheet);
            LoadTimeSheetList();
        }

        private void CreateCheckIn()
        {
            currentTimeSheet = new TimeSheet(AutoGenerateID(), CurrentUser.Ins.EmployeeIns.ID,
                Utils.EMPTY_DATETIME, Utils.EMPTY_DATETIME, "");
        }

        private string AutoGenerateID()
        {
            string checkInOutID;
            Random random = new Random();
            do
            {
                int number = random.Next(999999);
                checkInOutID = $"CIO{number:000000}";
            } while (timeSheetsDao.SearchByID(checkInOutID) != null);
            return checkInOutID;
        }

        private void OpenCheckOutDialog()
        {
            var inputService = new InputDialogService<TimeSheet>(new CheckOutDialog(), currentTimeSheet, Update);
            inputService.Show();
        }

        private void Update(TimeSheet timeSheet)
        {
            CheckOutTime = DateTime.Now;
            timeSheetsDao.Update(timeSheet);
            LoadTimeSheetList();
        }
    }
}