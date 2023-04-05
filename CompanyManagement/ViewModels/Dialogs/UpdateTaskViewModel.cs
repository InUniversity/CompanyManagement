using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.ViewModels.UserControls;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.Dialogs
{
    public interface IUpdateTask
    {
        ITasksInProject ParentDataContext { set; } 
        ITaskInput TaskInputDataContext { get; }
    }
    
    public class UpdateTaskViewModel : BaseViewModel, IUpdateTask
    {

        public ICommand UpdateTaskCommand { get; set; }

        public ITasksInProject ParentDataContext { get; set; }
        public ITaskInput TaskInputDataContext { get; set; }

        public UpdateTaskViewModel()
        {
            TaskInputDataContext = new TaskInputViewModel();
            SetCommands();
        }

        private void SetCommands()
        {
            UpdateTaskCommand = new RelayCommand<Window>(UpdateCommand);
        }

        private void UpdateCommand(Window inputWindow)
        {
            TaskInputDataContext.TrimAllTexts();
            if (!TaskInputDataContext.CheckAllFields())
                return;
            TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Update(task);
            inputWindow.Close();
        }
    }
}
