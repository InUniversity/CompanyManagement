using System.Windows;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class UpdateTaskInProjectViewModel: BaseViewModel
    {

        public ICommand UpdateTaskInProjectCommand { get; set; }

        public TasksInProjectViewModel ParentDataContext { get; set; }

        public TaskInProjectInputViewModel TaskInProjectInputDataContext { get; set; }

        public UpdateTaskInProjectViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            TaskInProjectInputDataContext = new TaskInProjectInputViewModel();
            UpdateTaskInProjectCommand = new RelayCommand<Window>(AddCommand, p => TaskInProjectInputDataContext.CheckAllFields());
        }

        private void AddCommand(Window inputwindow)
        {

        }
    }
}
