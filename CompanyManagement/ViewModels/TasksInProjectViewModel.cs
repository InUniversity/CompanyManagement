using CompanyManagement.Database;
using CompanyManagement.Dialogs;
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

        ICommand OpenTaskInProjectInputCommand { get; set; }
        ICommand DeleteTaskInProjectCommand { get; set; }
        ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        
        public TasksInProjectViewModel()
        {
            SetCommands();
        }

        private void LoadTaskInProjects()
        {
            TasksInProject = new ObservableCollection<TaskInProject>()
        }    

        private void SetCommands()
        {
            OpenTaskInProjectInputCommand = new RelayCommand<TaskInProject>(ExecuteAddCommand);
            DeleteTaskInProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTaskInProjectCommand = new RelayCommand<TaskInProject>(ExecuteUpdateCommand);
        }

        public void Add(TaskInProject taskInProject)
        {
            taskInProjectDao.Add(taskInProject);
        }

        public void ShowWithID(string projectID)
        {
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.SearchByProjectID(projectID));
        }

        private void ExecuteAddCommand(TaskInProject task)
        {
            AddTaskInProject addTaskInProjectDialog = new AddTaskInProject();
            
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
