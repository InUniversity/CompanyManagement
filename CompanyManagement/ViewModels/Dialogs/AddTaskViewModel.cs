using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

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

        private void SetCommands()
        {
            TaskInputDataContext = new TaskInputViewModel(new ProjectAssignmentDao(), new TaskStatusDao());
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
