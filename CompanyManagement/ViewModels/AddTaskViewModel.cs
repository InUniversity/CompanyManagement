using CompanyManagement.Models;
using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class AddTaskViewModel: BaseViewModel
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
            TaskInputDataContext = new TaskInputViewModel();
            AddTaskCommand = new RelayCommand<Window>(AddCommand, p => TaskInputDataContext.CheckAllFields());
        }

        private void AddCommand(Window inputwindow)
        {
            TaskInputDataContext.TrimmAllTexts();
            TaskInProject task = TaskInputDataContext.CreateTaskInProjectInstance();
            ParentDataContext.Add(task);
            inputwindow.Close();
        }
    }
}
