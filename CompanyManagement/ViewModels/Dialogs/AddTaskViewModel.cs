using System;
using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTaskViewModel : BaseViewModel, IInputViewModel<TaskInProject>
    {
        public ICommand AddTaskCommand { get; set; }

        public ITaskInput TaskInputDataContext { get; set; }
        private Action<TaskInProject> submitObjectAction;

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
                   TaskInProject task = TaskInputDataContext.Task;
                   submitObjectAction?.Invoke(task);
                   inputWindow.Close();
               }, null);
            dialog.Show();          
        }
        
        public void ReceiveObject(TaskInProject task)
        {
            TaskInputDataContext.Task = task;
        }

        public void ReceiveSubmitAction(Action<TaskInProject> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
