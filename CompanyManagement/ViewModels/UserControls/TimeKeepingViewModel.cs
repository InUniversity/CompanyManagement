using System.Collections.Generic;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.Dialogs;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using System;
using System.Linq;
using System.Globalization;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeKeepingViewModel : BaseViewModel, IEditDBViewModel, IRetrieveProjectID
    {

        private List<TimeKeeping> timeKeepingSet; 
        public List<TimeKeeping> TimeKeepingSet { get => timeKeepingSet; set { timeKeepingSet = value; OnPropertyChanged(); } }

        private DateTime timeKeepingDateSelected;
        public DateTime TimeKeepingDateSelected { get => timeKeepingDateSelected; set { timeKeepingDateSelected = value; OnPropertyChanged(); FilterDate(); } }

       

        public ICommand OpenTimeKeepingInputCommand { get; set; }
        public ICommand DeleteTimeKeepingCommand { get; set; }
        public ICommand UpdateTimeKeepingCommand { get; set; }
        public ICommand NextTimeKeepingDate { get; set; }
        public ICommand BackTimeKeepingDate { get; set; }

        private TimeKeepingDao timeKeepingDao;

        private string projectID = "";
        private string currentEmployeeID = CurrentUser.Instance.CurrentEmployee.ID;

        public TimeKeepingViewModel()
        {
            timeKeepingDateSelected = DateTime.Now;
            timeKeepingDao = new TimeKeepingDao();
            SetCommands();
        }
        
        private void LoadTimeKeeping()
        {
            List<TimeKeeping> timeKeepingSet = CurrentUser.Instance.IsEmployee()
                ? timeKeepingDao.SearchByEmployeeID(projectID, currentEmployeeID)
                : timeKeepingDao.SearchByProjectID(projectID);
            TimeKeepingSet = timeKeepingSet;
        }
        private void FilterDate()
        {
            LoadTimeKeeping();
            var allItem = TimeKeepingSet;
            Log.Instance.Information("Timkeeping","selected date = "+ timeKeepingDateSelected.ToShortDateString());
            allItem = TimeKeepingSet
                    .Where(item => item.Start.ToShortDateString() == timeKeepingDateSelected.ToShortDateString())
                    .ToList();
            TimeKeepingSet = new List<TimeKeeping>(allItem);
        }
        private void SetCommands()
        {
            OpenTimeKeepingInputCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteTimeKeepingCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTimeKeepingCommand = new RelayCommand<TimeKeeping>(ExecuteUpdateCommand);
            NextTimeKeepingDate = new RelayCommand<object>(ExecuteNextTimeKeepingDate);
            BackTimeKeepingDate = new RelayCommand<object>(ExecuteBackTimeKeepingDate);
        }

        private void ExecuteBackTimeKeepingDate(object obj)
        {
            TimeKeepingDateSelected = TimeKeepingDateSelected.AddDays(-1);
        }

        private void ExecuteNextTimeKeepingDate(object obj)
        {
            TimeKeepingDateSelected = TimeKeepingDateSelected.AddDays(1);
        }

        private void ExecuteAddCommand(object p)
        {
            AddTimeKeepingDialog addTimeKeepingDialog = new AddTimeKeepingDialog();
            IDialogViewModel addTimeKeepingViewModel = (IDialogViewModel)addTimeKeepingDialog.DataContext;
            addTimeKeepingViewModel.ParentDataContext = this;
            TimeKeeping timeKeeping = CreateTimeKeeping();
            addTimeKeepingViewModel.Retrieve(timeKeeping);
            addTimeKeepingDialog.ShowDialog();
        }

        private TimeKeeping CreateTimeKeeping()
        {
            // TODO
            return new TimeKeeping();
        }

        private void ExecuteDeleteCommand(string id)
        {
            timeKeepingDao.Delete(id);
            FilterDate();
        }

        private void ExecuteUpdateCommand(TimeKeeping timeKeeping)
        {
            UpdateTimeKeepingDialog updateTimeKeepingDialog = new UpdateTimeKeepingDialog();
            IDialogViewModel updateTaskViewModel = (IDialogViewModel)updateTimeKeepingDialog.DataContext;
            updateTaskViewModel.ParentDataContext = this;
            updateTaskViewModel.Retrieve(timeKeeping);
            updateTimeKeepingDialog.ShowDialog();
        }

        public void AddToDB(object timeKeeping)
        {
            timeKeepingDao.Add(timeKeeping as TimeKeeping);
            FilterDate();
        }

        public void UpdateToDB(object timeKeeping)
        {
            timeKeepingDao.Update(timeKeeping as TimeKeeping);
            FilterDate();
        }

        public void RetrieveProjectID(string projectID)
        {
            this.projectID = projectID;
            FilterDate();
        }
    }
}