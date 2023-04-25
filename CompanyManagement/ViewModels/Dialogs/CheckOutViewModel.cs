﻿using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using System;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckOutViewModel : BaseViewModel, IInputViewModel<CheckInOut>
    {
        public CheckInOutInputViewModel CheckInOutInputDataContext { get; }

        private Action<CheckInOut> submitObjectAction;

        private ObservableCollection<TaskInProject> tasksCheckOut;
        public ObservableCollection<TaskInProject> TasksCheckOut { get => tasksCheckOut; set => tasksCheckOut = value; }

        private List<TaskInProject> TasksCanChoose;

        private ObservableCollection<TaskInProject> searchedTasksCanChoose;
        public ObservableCollection<TaskInProject> SearchedTasksCanChoose { get => searchedTasksCanChoose; set { searchedTasksCanChoose = value; OnPropertyChanged(); }  }

        private List<TaskInProject> selectedTasks;
        public List<TaskInProject> SelectedTasks { get => selectedTasks; set => selectedTasks = value; }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand CheckOutCommand { get; set; }
        public ICommand AddTaskCompletedCommand { get; set; }
        public ICommand GetAllSelectedTasksCommand { get; set; }
        public ICommand DeleteTaskCompletedCommand { get; set; }

        private TaskCheckOutDao taskCheckOutDao = new TaskCheckOutDao();
        private TaskInProjectDao taskInProjectDao = new TaskInProjectDao();

        public CheckOutViewModel()
        {
            SetCommands();
            LoadTasksCanChoose();
            CheckInOutInputDataContext = new CheckInOutInputViewModel();
            TasksCheckOut = new ObservableCollection<TaskInProject>();
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
            AlertDialogService dialog = new AlertDialogService(
                 "Check out",
                 "Bạn có chắc chắn muốn check out không ?",
                 () =>
                 {
                     CheckInOut checkOut = CheckInOutInputDataContext.CreateCheckInOutInstance();
                     CommitTasksCheckOutToDB();
                     submitObjectAction?.Invoke(checkOut);
                     window.Close();
                 }, () => { });
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
                foreach (TaskInProject task in SelectedTasks)
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
            //var list = completedTaskDao.GetOpenAssignedTasks(CheckInOutInputDataContext.EmployeeID,
            //    Utils.ToFormatSQLServer(CheckInOutInputDataContext.CheckOutTime));
            //TasksCanChoose = new List<TaskInProject>(list);
            //SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(TasksCanChoose);
            TasksCanChoose = new List<TaskInProject>(new TaskInProjectDao().SearchByProjectID("PRJ001"));
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

        private void CommitTasksCheckOutToDB()
        {
            foreach (var task in TasksCheckOut)
            {
                UpdateTaskInProjectToDB(task);
                AddTaskCheckOutToDB(task);
            }
            LoadTasksCanChoose();
        }

        private void UpdateTaskInProjectToDB(TaskInProject task)
        {
            taskInProjectDao.Update(task);
        }

        private void AddTaskCheckOutToDB(TaskInProject task)
        {
            TaskCheckOut taskCheckOut = new TaskCheckOut(CheckInOutInputDataContext.ID, task.ID, DateTime.Now, task.Progress, task);
            taskCheckOutDao.Add(taskCheckOut);
        }

        public void ReceiveObject(CheckInOut checkInOut)
        {
            CheckInOutInputDataContext.Receive(checkInOut);
        }

        public void ReceiveSubmitAction(Action<CheckInOut> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
