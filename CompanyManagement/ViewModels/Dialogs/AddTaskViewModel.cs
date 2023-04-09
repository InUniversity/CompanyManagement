﻿using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTaskViewModel : BaseViewModel, IDialogViewModel
    {

        public ICommand AddTaskCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }
        public ITaskInput TaskInputDataContext { get; set; }

        public AddTaskViewModel()
        {
            TaskInputDataContext = new TaskInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            AddTaskCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputWindow)
        {
            TaskInputDataContext.TrimAllTexts();
            AlertDialogService dialog = new AlertDialogService(
               "Thêm nhiệm vụ",
               "Bạn chắc chắn muốn thêm nhiệm vụ !",
               () =>
               {
                   TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
                   ParentDataContext.AddToDB(task);
               }, () => { });
            dialog.Show();          
            inputWindow.Close();
        }
        
        public void Retrieve(object task)
        {
            TaskInputDataContext.RetrieveTask(task as TaskInProject); 
        }
    }
}
