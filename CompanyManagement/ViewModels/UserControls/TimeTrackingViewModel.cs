using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TimeTrackingViewModel : BaseViewModel, IRetrieveProjectID
    {
        private List<TaskCheckOut> originalTaskCheckOutList;
        
        private List<TaskCheckOut> searchedTasksCheckOut;
        public List<TaskCheckOut> SearchedTasksCheckOut 
        { get => searchedTasksCheckOut; set { searchedTasksCheckOut = value; OnPropertyChanged(); } }

        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate 
        { get => selectedDate; set { selectedDate = value; OnPropertyChanged(); SearchByDate(); } }
        
        public ICommand BackDateCommand { get; private set; }
        public ICommand NextDateCommand { get; private set; }

        private TaskCheckOutDao taskCheckOutDao = new TaskCheckOutDao();

        private string projectID;

        public TimeTrackingViewModel()
        {
            SetCommands();
        }

        private void LoadTaskCheckOutList()
        {
            originalTaskCheckOutList = taskCheckOutDao.SearchByProjectID(projectID);
            SearchedTasksCheckOut = originalTaskCheckOutList;
        }
        
        private void SetCommands()
        {
            BackDateCommand = new RelayCommand<object>(ExecuteBackDate);
            NextDateCommand = new RelayCommand<object>(ExecuteNextDate);
        }

        private void ExecuteBackDate(object obj)
        {
            SelectedDate = SelectedDate.AddDays(-1);
        }

        private void ExecuteNextDate(object obj)
        {
            SelectedDate = SelectedDate.AddDays(1);
        }
        
        private void SearchByDate()
        {
            SearchedTasksCheckOut = originalTaskCheckOutList
                .Where(item => item.UpdateDate.ToShortDateString() == SelectedDate.ToShortDateString())
                .ToList();
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadTaskCheckOutList();
        }
    }
}