using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInViewModel : BaseViewModel, IInputViewModel<TimeSheet>
    {
        public TimeSheetInputViewModel CheckInOutInputDataContext { get; }

        private Action<TimeSheet> submitObjectAction;

        private TaskInProject selectedTask ;
        public TaskInProject SelectedTask { get => selectedTask; set => selectedTask = value; }

        private ObservableCollection<TaskInProject> startingTask;
        public ObservableCollection<TaskInProject> StartingTask { get => startingTask; set => startingTask = value; }

        private List<TaskInProject> TasksCanChoose;

        private ObservableCollection<TaskInProject> searchedTasksCanChoose;
        public ObservableCollection<TaskInProject> SearchedTasksCanChoose { get => searchedTasksCanChoose; set { searchedTasksCanChoose = value; OnPropertyChanged(); }  }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged();} }

        public ICommand CheckInCommand { get; private set; }
        public ICommand ChooseTaskCommand { get; private set; }
        public ICommand DeleteSelectedTaskCommand { get; private set; }
        public ICommand GetSelectedTaskCommand { get; private set; }
        public ICommand CloseDialogCommand { get; private set; }

        private TasksDao tasksDao = new TasksDao();

        public CheckInViewModel()
        {
            CheckInOutInputDataContext = new TimeSheetInputViewModel();
            StartingTask = new ObservableCollection<TaskInProject>();
            LoadTasksCanChoose();
            SetCommands();
        }

        private void SetCommands()
        {
            CheckInCommand = new RelayCommand<Window>(ExecuteCheckInCommand);
            ChooseTaskCommand = new RelayCommand<object>(ExecuteChooseTaskCommand);
            DeleteSelectedTaskCommand = new RelayCommand<TaskInProject>(ExecuteDeleteSelectedTaskCommand);
            GetSelectedTaskCommand = new RelayCommand<ListView>(ExecuteGetSelectedTaskCommand);
            CloseDialogCommand = new RelayCommand<Window>(ExecuteCloseCommand);
        }

        private void ExecuteCloseCommand(Window window)
        {
            window.Close();
        }

        private void ExecuteGetSelectedTaskCommand(ListView listView)
        {
            if (listView.SelectedItem == null) return;
            SelectedTask = listView.SelectedItem as TaskInProject;
        }

        private void ExecuteDeleteSelectedTaskCommand(TaskInProject task)
        {
            StartingTask.Remove(task);
            selectedTask = null;
        }

        private void ExecuteChooseTaskCommand(object obj)
        {
            if (SelectedTask == null) return;
            StartingTask.Clear();
            StartingTask.Add(SelectedTask);
            LoadTasksCanChoose();
            SelectedTask = new TaskInProject();
        }

        private bool CheckTask()
        {
            ErrorMessage = "";
            if (selectedTask == null)
            {
                ErrorMessage = Utils.invalidCheckInMess;
                return false;
            }
            return true;
        }

        private void ExecuteCheckInCommand(Window window)
        {
            if (!CheckTask()) return;
            var dialog = new AlertDialogService(
               "Check in",
               "Bạn chắc chắn muốn check in !",
               () =>
               {
                   TimeSheet checkIn = CheckInOutInputDataContext.TimeSheetIns;
                   submitObjectAction?.Invoke(checkIn);
                   window.Close();
               }, null);
            dialog.Show();
        }

        private void LoadTasksCanChoose()
        {
            TasksCanChoose = tasksDao.SearchTasksCheckOut(CurrentUser.Ins.EmployeeIns.ID, DateTime.Now);
            SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(TasksCanChoose);
        }

        private void SearchByName()
        {
            var searchedItems = TasksCanChoose;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = TasksCanChoose
                    .Where(item => item.Title.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(searchedItems);
        }

        public void ReceiveObject(TimeSheet request)
        {
            CheckInOutInputDataContext.TimeSheetIns = request;
        }

        public void ReceiveSubmitAction(Action<TimeSheet> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
