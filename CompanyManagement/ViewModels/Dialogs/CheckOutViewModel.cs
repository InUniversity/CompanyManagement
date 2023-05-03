using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using System;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckOutViewModel : BaseViewModel, IInputViewModel<TimeSheet>
    {
        public TimeSheetInputViewModel CheckInOutInputDataContext { get; }

        private Action<TimeSheet> submitObjectAction;

        private ObservableCollection<TaskInProject> tasksCheckOut;
        public ObservableCollection<TaskInProject> TasksCheckOut { get => tasksCheckOut; set => tasksCheckOut = value; }

        private List<TaskInProject> TasksCanChoose;

        private ObservableCollection<TaskInProject> searchedTasksCanChoose;
        public ObservableCollection<TaskInProject> SearchedTasksCanChoose { get => searchedTasksCanChoose; set { searchedTasksCanChoose = value; OnPropertyChanged(); }  }

        private List<TaskInProject> selectedTasks;
        public List<TaskInProject> SelectedTasks { get => selectedTasks; set => selectedTasks = value; }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand CheckOutCommand { get; private set; }
        public ICommand AddTaskCompletedCommand { get; private set; }
        public ICommand GetAllSelectedTasksCommand { get; private set; }
        public ICommand DeleteTaskCompletedCommand { get; private set; }

        private TaskCheckOutsDao taskCheckOutsDao = new TaskCheckOutsDao();
        private TasksDao tasksDao = new TasksDao();

        public CheckOutViewModel()
        {
            CheckInOutInputDataContext = new TimeSheetInputViewModel();
            TasksCheckOut = new ObservableCollection<TaskInProject>();
            SetCommands();
            LoadTasksCanChoose();
        }

        private void SetCommands()
        {
            CheckOutCommand = new RelayCommand<Window>(ExecuteCheckOutCommand);
            GetAllSelectedTasksCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedTasksCommand);
            AddTaskCompletedCommand = new RelayCommand<object>(ExecuteAddTaskCheckOutCommand);
            DeleteTaskCompletedCommand = new RelayCommand<TaskInProject>(ExecuteDeleteTaskCheckOutCommand);
        }

        private void ExecuteCheckOutCommand(Window window)
        {
            var dialog = new AlertDialogService(
                 "Check out",
                 "Bạn có chắc chắn muốn check out không ?",
                 () =>
                 {
                     TimeSheet checkOut = CheckInOutInputDataContext.TimeSheetIns;
                     AddTasksCheckOut();
                     submitObjectAction?.Invoke(checkOut);
                     window.Close();
                 }, null);
            dialog.Show();
        }

        private void ExecuteDeleteTaskCheckOutCommand(TaskInProject task)
        {
            TasksCheckOut.Remove(task);
            LoadTasksCanChoose();
        }

        private void ExecuteAddTaskCheckOutCommand(object b)
        {
            if (SelectedTasks != null)
            {
                foreach (var task in SelectedTasks)
                {
                    TasksCheckOut.Add(task);
                }
                LoadTasksCanChoose();
            }
            SelectedTasks = new List<TaskInProject>();
        }

        private void ExecuteGetAllSelectedTasksCommand(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<TaskInProject>().ToList();
            SelectedTasks = selectedItems;
        }

        private void LoadTasksCanChoose()
        {
            TasksCanChoose = tasksDao.SearchCurrentTasksByEmployeeID(CurrentUser.Ins.EmployeeIns.ID, DateTime.Now);
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

        private void AddTasksCheckOut()
        {
            foreach (var task in TasksCheckOut)
            {
                UpdateTaskInProject(task);
                AddTaskCheckOut(task);
            }
            LoadTasksCanChoose();
        }

        private void UpdateTaskInProject(TaskInProject task)
        {
            tasksDao.Update(task);
        }

        private void AddTaskCheckOut(TaskInProject task)
        {
            TaskCheckOut taskCheckOut = new TaskCheckOut(CheckInOutInputDataContext.ID, task.ID, DateTime.Now, task.Progress, task);
            taskCheckOutsDao.Add(taskCheckOut);
        }

        public void ReceiveObject(TimeSheet timeSheet)
        {
             CheckInOutInputDataContext.TimeSheetIns = timeSheet;
        }

        public void ReceiveSubmitAction(Action<TimeSheet> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
