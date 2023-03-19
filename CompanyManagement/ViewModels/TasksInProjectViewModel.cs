using CompanyManagement.Database;
using CompanyManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel : BaseViewModel, IShowTasksInProject
    {

        private ObservableCollection<TaskInProject> tasksInProject;
        public ObservableCollection<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private string projectID;
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        ICommand OpenTaskInProjectInoutCommand { get; set; }
        ICommand DeleteTaskInProjectCommand { get; set; }
        ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        
        public TasksInProjectViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            
        }

        public void ShowWithID(string projectID)
        {
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.SearchByProjectID(projectID));
        }

        private void ExecuteAddCommand(TaskInProject task)
        {
            
        }
        
        private void ExecuteDeleteCommand(string id)
        {

        }

        private void ExecuteUpdateCommand(TaskInProject task)
        {
           
        }
    }

    public interface IShowTasksInProject
    {
        void ShowWithID(string projectID);
    }
}
