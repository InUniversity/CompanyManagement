using System.Collections.Generic;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.Views.Dialogs;
using System.Windows.Input;
using CompanyManagement.Database;

namespace CompanyManagement.ViewModels.UserControls
{

    public interface ITimeKeeping
    {
        void Add(TimeKeeping timeKeeping);
        void Update(TimeKeeping timeKeeping);
    }

    public class TimeKeepingViewModel : BaseViewModel, ITimeKeeping, IRetrieveProjectID
    {

        private List<TimeKeeping> timeKeepingSet; 
        public List<TimeKeeping> TimeKeepingSet { get => timeKeepingSet; set { timeKeepingSet = value; OnPropertyChanged(); } }

        public ICommand OpenTimeKeepingInputCommand { get; set; }
        public ICommand DeleteTimeKeepingCommand { get; set; }
        public ICommand UpdateTimeKeepingCommand { get; set; }

        private TimeKeepingDao timeKeepingDao;

        private string projectID = "";

        public TimeKeepingViewModel()
        {
            timeKeepingDao = new TimeKeepingDao();
            LoadTimeKeeping();
            SetCommands();
        }

        private void LoadTimeKeeping()
        {
            TimeKeepingSet = timeKeepingDao.SearchByProjectID(projectID);
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
            AddTimeKeepingViewModel addTimeKeepingViewModel = (AddTimeKeepingViewModel)addTimeKeepingDialog.DataContext;
            addTimeKeepingViewModel.ParentDataContext = this;
            TimeKeeping timeKeeping = CreateTimeKeeping();
            addTimeKeepingViewModel.TimeKeepingInputDataContext.Retrieve(timeKeeping);
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

        public void RetrieveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadTimeKeeping();
        }
    }
}