using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    public interface IAddTask
    {
        ITasksInProject ParentDataContext { get; set; } 
        ITaskInput TaskInputDataContext { get; set; } 
    }

    public class AddTaskViewModel : BaseViewModel, IAddTask
    {

        public ICommand AddTaskCommand { get; set; }

        public ITasksInProject ParentDataContext { get; set; }
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
            TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Add(task);
            inputWindow.Close();
        }
    }
}
