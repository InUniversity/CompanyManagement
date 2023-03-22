using CompanyManagement.Database.Implementations;
using CompanyManagement.Dialogs;
using CompanyManagement.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel : BaseViewModel, ITasksInProject, IRetrieveProjectID
    {

        private ObservableCollection<TaskInProject> tasksInProject;
        public ObservableCollection<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private string projectID = "";
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        
        public TasksInProjectViewModel()
        {
            LoadTaskInProjects();
            SetCommands();
        }

        public TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), SingletonAccount.Instance.CurrentAccount.EmployeeId, ProjectID);
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

        public void Add(TaskInProject task)
        {
            taskInProjectDao.Add(task);
            LoadTaskInProjects();
        }

        public void Update(TaskInProject task)
        {
            taskInProjectDao.Update(task);
            LoadTaskInProjects();
        }

        public void ShowWithID(string projectID)
        {
            this.projectID = projectID;
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.SearchByProjectID(projectID));
        }

        private void ExecuteAddCommand(TaskInProject task)
        {
            AddTaskDialog addTaskDialog = new AddTaskDialog();
            AddTaskViewModel addTaskViewModel = (AddTaskViewModel)addTaskDialog.DataContext;
            addTaskViewModel.ParentDataContext = this;
            task = CreateTaskInProjectInstance();
            addTaskViewModel.TaskInputDataContext.Retrieve(task);
            addTaskDialog.ShowDialog();
        }
        
        private void ExecuteDeleteCommand(string id)
        {
            taskInProjectDao.Delete(id);
            LoadTaskInProjects();
        }

        private void ExecuteUpdateCommand(TaskInProject task)
        {
            UpdateTaskDialog updateTaskDialog = new UpdateTaskDialog();
            UpdateTaskViewModel updateTaskViewModel = (UpdateTaskViewModel)updateTaskDialog.DataContext;
            updateTaskViewModel.ParentDataContext = this;
            updateTaskViewModel.TaskInputDataContext.Retrieve(task);
            updateTaskDialog.ShowDialog();
        }

        private string AutoGenerateID()
        {
            string taskInProjectID;
            Random random = new Random();
            do
            {
                int number = random.Next(1000000);
                taskInProjectID = $"T{number:000000}";
            } while (taskInProjectDao.SearchByID(taskInProjectID) != null);
            return taskInProjectID;
        }
    }

    public interface ITasksInProject
    {
        void Add(TaskInProject task);
        void Update(TaskInProject task); 
    }
    
    public interface IRetrieveProjectID
    {
        void ShowWithID(string projectID);
    }
}
