using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInOutViewModel : BaseViewModel, IEditDBViewModel
    {
        private bool isToggled;
        public bool IsToggled { get => isToggled; set { isToggled = value; OnPropertyChanged(); } }

        private CheckInOut currentCheckInOut = new CheckInOut();
        
        private ObservableCollection<CheckInOut> checkInOutList;
        public ObservableCollection<CheckInOut> CheckInOutList { get => checkInOutList; set { checkInOutList = value; } }
        
        public ICommand ToggledCommand { get; set; }

        private CheckInOutDao checkInOutDao = new CheckInOutDao();

        public CheckInOutViewModel()
        {
            LoadCheckInOutList();
            ToggledCommand = new RelayCommand<object>(Toggled);
        }

        private void LoadCheckInOutList()
        {
            CheckInOutList = new ObservableCollection<CheckInOut>(checkInOutDao.GetAll());
        }

        private void Toggled(object obj)
        {
            OpenCheckInDialog();
            // if (IsToggled) OpenCheckInDialog();
            // else OpenCheckOutDialog();
            // if (!string.IsNullOrWhiteSpace(currentCheckInOut.TaskID))
            //     IsToggled = !IsToggled;
        }

        private void OpenCheckInDialog()
        {
            var checkInDialog = new CheckInDialog();
            var checkInViewModel = checkInDialog.ViewModel;
            checkInViewModel.ParentDataContext = this;
            CreateNewCheckIn();
            checkInViewModel.Retrieve(currentCheckInOut);
            checkInDialog.ShowDialog();
        }

        private void CreateNewCheckIn()
        {
            currentCheckInOut = new CheckInOut(AutoGenerateID(), CurrentUser.Instance.CurrentEmployee.ID,
                Utils.EMPTY_DATETIME, Utils.EMPTY_DATETIME, false, "", "");
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
            var checkOutDialog = new CheckOutDialog();
            var checkInViewModel = (IDialogViewModel)checkOutDialog.DataContext;
            checkInViewModel.ParentDataContext = this;
            checkInViewModel.Retrieve(currentCheckInOut);
            checkOutDialog.ShowDialog();
        }

        public void AddToDB(object checkInOut)
        {
            checkInOutDao.Add(checkInOut as CheckInOut);
        }

        public void UpdateToDB(object checkInOut)
        {
            checkInOutDao.Update(checkInOut as CheckInOut);
        }
    }
}