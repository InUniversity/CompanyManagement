﻿using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;

namespace CompanyManagement.ViewModels
{
    public class UpdateTaskViewModel: BaseViewModel
    {

        public ICommand UpdateTaskCommand { get; set; }

        public TasksInProjectViewModel ParentDataContext { get; set; }

        public TaskInputViewModel TaskInputDataContext { get; set; }

        public UpdateTaskViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            TaskInputDataContext = new TaskInputViewModel(new ProjectAssignmentDao());
            UpdateTaskCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputwindow)
        {
            TaskInputDataContext.TrimmAllTexts();
            TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Update(task);
            inputwindow.Close();
        }
    }
}
