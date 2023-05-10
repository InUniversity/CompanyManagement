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
    public class UpdateTaskViewModel : BaseViewModel, IInputViewModel<TaskInProject>
    {
        public ICommand UpdateTaskCommand { get; set; }
        public ICommand CloseDialogCommand { get; set; }

        public ITaskInput TaskInputDataContext { get; set; }
        private Action<TaskInProject> submitObjectAction;

        public UpdateTaskViewModel()
        {
            TaskInputDataContext = new TaskInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            UpdateTaskCommand = new RelayCommand<Window>(UpdateCommand);
            CloseDialogCommand = new RelayCommand<Window>(CloseCommand);
        }

        private void CloseCommand(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
                "Cập nhật nhiệm vụ",
                "Bạn chắc chắn muốn thoát !",
                () =>
                {
                    window.Close();
                }, () => { });
            dialog.Show();
        }

        private void UpdateCommand(Window inputWindow)
        {
            TaskInputDataContext.TrimAllTexts();
            if (!TaskInputDataContext.CheckAllFields())
                return;
            AlertDialogService dialog = new AlertDialogService(
              "Cập nhật nhiệm vụ",
              "Bạn chắc chắn muốn cập nhật nhiệm vụ !",
              () =>
              {
                  TaskInProject task = TaskInputDataContext.TaskInProjectIns;
                  submitObjectAction?.Invoke(task);
                  inputWindow.Close();
              }, () => { });
            dialog.Show();        
        }
        
        public void ReceiveObject(TaskInProject task)
        {
            TaskInputDataContext.TaskInProjectIns = task;
        }

        public void ReceiveSubmitAction(Action<TaskInProject> submitObjectAction)
        {
            this.submitObjectAction = submitObjectAction;
        }
    }
}
