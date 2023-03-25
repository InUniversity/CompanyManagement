using CompanyManagement.Models;
using System;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.ViewModels.Dialogs
{
    public class AddTaskViewModel : BaseViewModel
    {

        public ICommand AddTaskCommand { get; set; }

        public TasksInProjectViewModel ParentDataContext { get; set; }

        public TaskInputViewModel TaskInputDataContext { get; set; }

        public AddTaskViewModel()
        {
            SetCommands();
        }

        public void SetCommands()
        {
            TaskInputDataContext = new TaskInputViewModel(new ProjectAssignmentDao());
            AddTaskCommand = new RelayCommand<Window>(AddCommand);
        }

        private void AddCommand(Window inputwindow)
        {
            TaskInputDataContext.TrimAllTexts();
            TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Add(task);
            inputwindow.Close();
        }
    }
}
