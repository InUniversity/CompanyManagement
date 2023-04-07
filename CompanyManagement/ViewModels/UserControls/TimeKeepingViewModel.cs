﻿using System.Collections.Generic;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeKeepingViewModel : BaseViewModel, IEditDBViewModel, IRetrieveProjectID
    {

        private List<TimeKeeping> timeKeepingSet; 
        public List<TimeKeeping> TimeKeepingSet { get => timeKeepingSet; set { timeKeepingSet = value; OnPropertyChanged(); } }

        public ICommand OpenTimeKeepingInputCommand { get; set; }
        public ICommand DeleteTimeKeepingCommand { get; set; }
        public ICommand UpdateTimeKeepingCommand { get; set; }

        private TimeKeepingDao timeKeepingDao;

        private string projectID = "";
        private Employee currentEmployee = CurrentUser.Instance.CurrentEmployee;

        public TimeKeepingViewModel()
        {
            timeKeepingDao = new TimeKeepingDao();
            LoadTimeKeeping();
            SetCommands();
        }

        private void LoadTimeKeeping()
        {
            List<TimeKeeping> timeKeepingSet = string.Equals(currentEmployee.PositionID, Utils.POSITION_ID_EMPLOYEE) ?
                timeKeepingDao.SearchByEmployeeID(projectID, currentEmployee.ID)
                : timeKeepingDao.SearchByProjectID(projectID);
            TimeKeepingSet = timeKeepingSet;
        }

        private void SetCommands()
        {
            OpenTimeKeepingInputCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteTimeKeepingCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTimeKeepingCommand = new RelayCommand<TimeKeeping>(ExecuteUpdateCommand);
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
            LoadTimeKeeping();
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
            LoadTimeKeeping();
        }

        public void UpdateToDB(object timeKeeping)
        {
            timeKeepingDao.Update(timeKeeping as TimeKeeping);
            LoadTimeKeeping();
        }

        public void RetrieveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadTimeKeeping();
        }
    }
}