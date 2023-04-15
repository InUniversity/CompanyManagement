﻿using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckInViewModel : BaseViewModel, IDialogViewModel
    {
        private CheckInOut checkIn;

        private ObservableCollection<TaskInProject> tasksCanChoose;
        public ObservableCollection<TaskInProject> TaskInProjects { get => tasksCanChoose; set => tasksCanChoose = value; }

        private ICommand CheckInCommand { get; set; }
        
        public IEditDBViewModel ParentDataContext { get; set; }

        private CompletedTaskDao completedTaskDao = new CompletedTaskDao();

        public CheckInViewModel()
        {
            LoadTasksCanChoose();
            CheckInCommand = new RelayCommand<Window>(CheckIn);
        }

        private void LoadTasksCanChoose()
        {
            //var list = completedTaskDao.GetOpenAssignedTasks(checkIn.EmployeeID, 
            //    Utils.ToFormatSQLServer(checkIn.CheckInTime));
            //TaskInProjects = new ObservableCollection<TaskInProject>(list);
        }

        private void CheckIn(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
                "Check in",
                "Bạn chắc chắn muốn check in không?",
                () =>
                {
                    checkIn.CheckInTime = DateTime.Now;
                    ParentDataContext.AddToDB(checkIn);
                    window.Close();
                }, () => { });
        }
        
        public void Retrieve(object obj)
        {
            checkIn = obj as CheckInOut;
        }
    }
}