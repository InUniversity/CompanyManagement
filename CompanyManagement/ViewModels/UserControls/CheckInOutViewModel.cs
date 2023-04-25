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
    public class CheckInOutViewModel : BaseViewModel
    {
        private bool isToggled;
        public bool IsToggled { get => isToggled; set { isToggled = value; OnPropertyChanged(); } }

        private CheckInOut currentCheckInOut;
        
        private ObservableCollection<CheckInOut> checkInOutList;
        public ObservableCollection<CheckInOut> CheckInOutList { get => checkInOutList; set { checkInOutList = value; } }

        private DateTime checkInTime;
        public DateTime CheckInTime { get => checkInTime; set { checkInTime = value; OnPropertyChanged(); } }
        
        private DateTime checkOutTime;
        public DateTime CheckOutTime { get => checkOutTime; set { checkOutTime = value; OnPropertyChanged(); } }

        public ICommand ToggledCommand { get; private set; }

        private CheckInOutDao checkInOutDao = new CheckInOutDao();
        private TaskCheckOutDao taskCheckOutDao = new TaskCheckOutDao();
        private TaskInProjectDao taskInProjectDao = new TaskInProjectDao();

        public CheckInOutViewModel()
        {
            LoadCheckInOutList();
            SetCommands();
        }

        private void LoadCheckInOutList()
        {
            //TODO
            CheckInOutList = new ObservableCollection<CheckInOut>(checkInOutDao.GetAll());
        }

        private void SetCommands()
        {
            ToggledCommand = new RelayCommand<object>(Toggled);
        }

        private void Toggled(object obj)
        {
            if (IsToggled) OpenCheckInDialog();
            else OpenCheckOutDialog();
            if (!string.IsNullOrWhiteSpace(currentCheckInOut.TaskCheckInID))
                IsToggled = !IsToggled;
        }

        private void OpenCheckInDialog()
        {
            CreateCheckIn();
            var inputService = new InputDialogService<CheckInOut>(new CheckInDialog(), currentCheckInOut, Add);
            inputService.Show();
        }

        private void Add(CheckInOut checkInOut)
        {
            CheckInTime = DateTime.Now;
            CheckOutTime = new DateTime();
            checkInOutDao.Add(checkInOut);
            LoadCheckInOutList();
        }

        private void CreateCheckIn()
        {
            currentCheckInOut = new CheckInOut(AutoGenerateID(), CurrentUser.Ins.EmployeeIns.ID,
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
            } while (checkInOutDao.SearchByID(checkInOutID) != null);
            return checkInOutID;
        }

        private void OpenCheckOutDialog()
        {
            var inputService = new InputDialogService<CheckInOut>(new CheckOutDialog(), currentCheckInOut, Update);
            inputService.Show();
        }

        private void Update(CheckInOut checkInOut)
        {
            CheckOutTime = DateTime.Now;
            checkInOutDao.Update(checkInOut);
            LoadCheckInOutList();
        }
    }
}