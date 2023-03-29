using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeKeepingViewModel : BaseViewModel, ITimeKeeping
    {

        private ObservableCollection<TimeKeeping> timeKeepingSet; //Set of TimeKeeping
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
            OpenTimeKeepingInputCommand = new RelayCommand<TaskInProject>(ExecuteAddCommand);
            DeleteTimeKeepingCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTimeKeepingCommand = new RelayCommand<TaskInProject>(ExecuteUpdateCommand);
        }


        private void ExecuteAddCommand(TaskInProject task)
        {
            /*
            try
            {
                AddTaskDialog addTaskDialog = new AddTaskDialog();
                AddTaskViewModel addTaskViewModel = (AddTaskViewModel)addTaskDialog.DataContext;
                addTaskViewModel.ParentDataContext = this;
                task = CreateTaskInProjectInstance();
                addTaskViewModel.TaskInputDataContext.Retrieve(task);
                addTaskDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void ExecuteDeleteCommand(string id)
        {
        }

        private void ExecuteUpdateCommand(TaskInProject task)
        {
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

    public interface ITimeKeeping
    {
        void Add(TimeKeeping timeKeeping);
        void Update(TimeKeeping timeKeeping);
    }
}
