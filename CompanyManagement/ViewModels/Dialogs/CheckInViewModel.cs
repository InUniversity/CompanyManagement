﻿using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Views.Dialogs.Interfaces;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Views.UserControls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInViewModel : BaseViewModel, IInputViewModel<CheckInOut>
    {
        public CheckInOutInputViewModel CheckInOutInputDataContext { get; }

        private Action<CheckInOut> submitObjectAction;

        private TaskInProject selectedTask ;
        public TaskInProject SelectedTask { get => selectedTask; set => selectedTask = value; }

        private ObservableCollection<TaskInProject> startingTask;
        public ObservableCollection<TaskInProject> StartingTask { get => startingTask; set => startingTask = value; }

        private List<TaskInProject> TasksCanChoose;

        private ObservableCollection<TaskInProject> searchedTasksCanChoose;
        public ObservableCollection<TaskInProject> SearchedTasksCanChoose { get => searchedTasksCanChoose; set { searchedTasksCanChoose = value; OnPropertyChanged(); }  }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand CheckInCommand { get; set; }
        public ICommand ChooseTaskCommand { get; set; }
        public ICommand DeleteSelectedTaskCommand { get; set; }
        public ICommand GetSelectedTaskCommand { get; set; }

        public CheckInViewModel()
        {
            LoadTasksCanChoose();
            SetCommands();
            CheckInOutInputDataContext = new CheckInOutInputViewModel();
            StartingTask = new ObservableCollection<TaskInProject>();
        }

        private void SetCommands()
        {
            CheckInCommand = new RelayCommand<Window>(ExecuteCheckInCommand);
            ChooseTaskCommand = new RelayCommand<object>(ExecuteChooseTaskCommand);
            DeleteSelectedTaskCommand = new RelayCommand<TaskInProject>(ExecuteDeleteSelectedTaskCommand);
            GetSelectedTaskCommand = new RelayCommand<ListView>(ExecuteGetSelectedTaskCommand);
        }

        private void ExecuteGetSelectedTaskCommand(ListView listView)
        {
            if (listView.SelectedItem != null)
            {
                var selectedItem = listView.SelectedItem as TaskInProject;
                SelectedTask = selectedItem;
            }
        }

        private void ExecuteDeleteSelectedTaskCommand(TaskInProject task)
        {
            StartingTask.Remove(task);
        }

        private void ExecuteChooseTaskCommand(object obj)
        {
            if (SelectedTask != null)
            {
                StartingTask.Clear();
                StartingTask.Add(SelectedTask);
                LoadTasksCanChoose();
                SelectedTask = new TaskInProject();
            }            
        }

        private void ExecuteCheckInCommand(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
               "Check in",
               "Bạn chắc chắn muốn check in không?",
               () =>
               {
                   CheckInOut checkIn = CheckInOutInputDataContext.CreateCheckInOutInstance();
                   submitObjectAction?.Invoke(checkIn);
                   window.Close();
               }, () => { });
            dialog.Show();
        }

        private void LoadTasksCanChoose()
        {
            //var list = completedTaskDao.GetOpenAssignedTasks(CheckInOutInputDataContext.EmployeeID,
            //    Utils.ToFormatSQLServer(CheckInOutInputDataContext.CheckInTime));
            //TasksCanChoose = new List<TaskInProject>(list);
            //SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(TasksCanChoose);
            TasksCanChoose = new List<TaskInProject>(new TaskInProjectDao().SearchCurrentTasksByEmployeeID(CurrentUser.Ins.EmployeeIns.ID));
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
