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

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        
        public TasksInProjectViewModel()
        {
            SetCommands();
        }

        private void LoadTaskInProjects()
        {
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.GetAll());
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
            LoadTaskInProjects();
        }

        public void Update(TaskInProject taskInProject)
        {
            taskInProjectDao.Update(taskInProject);
            LoadTaskInProjects();
        }

        public void ShowWithID(string projectID)
        {
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.SearchByProjectID(projectID));
        }

        private void ExecuteAddCommand(TaskInProject task)
        {
            AddTaskInProjectDialog addTaskInProjectDialog = new AddTaskInProjectDialog();
            AddTaskInProjectViewModel addTaskInProjectVM = (AddTaskInProjectViewModel)addTaskInProjectDialog.DataContext;
            addTaskInProjectVM.ParentDataContext = this;
            addTaskInProjectDialog.ShowDialog();
            
        }
        
        private void ExecuteDeleteCommand(string id)
        {
            taskInProjectDao.Delete(id);
            LoadTaskInProjects();
        }

        private void ExecuteUpdateCommand(TaskInProject task)
        {
            UpdateTaskInProjectDialog updateTaskInProjectDialog = new UpdateTaskInProjectDialog();
            UpdateTaskInProjectViewModel taskInProjectViewModel = (UpdateTaskInProjectViewModel)updateTaskInProjectDialog.DataContext;
            taskInProjectViewModel.ParentDataContext = this;
            taskInProjectViewModel.TaskInProjectInputDataContext.Retrieve(task);
            updateTaskInProjectDialog.ShowDialog();
        }
    }

    public interface IShowTasksInProject
    {
        void Add(TaskInProject taskInProject);
        void Update(TaskInProject taskInProject);   
        void ShowWithID(string projectID);
    }
}
