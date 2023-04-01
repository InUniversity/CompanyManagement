using CompanyManagement.Database.Implementations;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{

    public interface ITimeKeeping
    {
        void Add(TimeKeeping timeKeeping);
        void Update(TimeKeeping timeKeeping);
    }

    public class TimeKeepingViewModel : BaseViewModel, ITimeKeeping
    {

        private ObservableCollection<TimeKeeping> timeKeepingSet; 
        public ObservableCollection<TimeKeeping> TimeKeepingSet { get => timeKeepingSet; set { timeKeepingSet = value; OnPropertyChanged(); } }

        public ICommand OpenTimeKeepingInputCommand { get; set; }
        public ICommand DeleteTimeKeepingCommand { get; set; }
        public ICommand UpdateTimeKeepingCommand { get; set; }

        private ITimeKeepingDao timeKeepingDao;

        public TimeKeepingViewModel(ITimeKeepingDao timeKeepingDao)
        {
            this.timeKeepingDao = timeKeepingDao;
            LoadTimeKeeping();
            SetCommands();
        }

        private void LoadTimeKeeping()
        {
            TimeKeepingSet = new ObservableCollection<TimeKeeping>(timeKeepingDao.GetAll());
        }

        private void SetCommands()
        {
            OpenTimeKeepingInputCommand = new RelayCommand<object>(ExecuteAddCommand);
            DeleteTimeKeepingCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTimeKeepingCommand = new RelayCommand<TimeKeeping>(ExecuteUpdateCommand);
        }


        private void ExecuteAddCommand(object p)
        {
            try
            {
                AddTimeKeepingDialog addTimeKeepingDialog = new AddTimeKeepingDialog();
                AddTimeKeepingViewModel addTimeKeepingViewModel = (AddTimeKeepingViewModel)addTimeKeepingDialog.DataContext;
                addTimeKeepingViewModel.ParentDataContext = this;
                addTimeKeepingViewModel.TimeKeepingInputDataContext.Retrieve(new TimeKeeping());
                addTimeKeepingDialog.ShowDialog();
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void ExecuteDeleteCommand(string id)
        {
            timeKeepingDao.Delete(id);
            LoadTimeKeeping();
        }

        private void ExecuteUpdateCommand(TimeKeeping timeKeeping)
        {
            UpdateTimeKeepingDialog updateTimeKeepingDialog = new UpdateTimeKeepingDialog();
            UpdateTimeKeepingViewModel updateTaskViewModel = (UpdateTimeKeepingViewModel)updateTimeKeepingDialog.DataContext;
            updateTaskViewModel.ParentDataContext = this;
            updateTaskViewModel.TimeKeepingInputDataContext.Retrieve(timeKeeping);
            updateTimeKeepingDialog.ShowDialog();
        }

        public void Add(TimeKeeping timeKeeping)
        {
            timeKeepingDao.Add(timeKeeping);
            LoadTimeKeeping();
        }

        public void Update(TimeKeeping timeKeeping)
        {
            timeKeepingDao.Update(timeKeeping);
            LoadTimeKeeping();
        }

        public void Delete(string taskID)
        {
            timeKeepingDao.Delete(taskID);
            LoadTimeKeeping();
        }
    }
}
