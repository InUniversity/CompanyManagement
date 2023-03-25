using System;
using CompanyManagement.Dialogs;
using CompanyManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel : BaseViewModel, ITasksInProject, IRetrieveProjectID
    {

        private ObservableCollection<TaskInProject> tasksInProject;
        public ObservableCollection<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private ObservableCollection<EmployeeAccount> employeesInProject;
        public ObservableCollection<EmployeeAccount> EmployeesInProject { get => employeesInProject; set { employeesInProject = value; OnPropertyChanged(); } }

        private static string projectID = "";

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        private ITaskInProjectDao taskInProjectDao;
        private IProjectAssignmentDao projectAssignmentDao;
        
        public TasksInProjectViewModel(ITaskInProjectDao taskInProjectDao, IProjectAssignmentDao projectAssignmentDao)
        {
            this.taskInProjectDao = taskInProjectDao;
            this.projectAssignmentDao = projectAssignmentDao;
            LoadTaskInProjects();
            SetCommands();
        }

        private TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), "", "", "","","", 
                SingletonEmployee.Instance.CurrentEmployeeAccount.ID,"", projectID);
        }

        private void LoadTaskInProjects()
        {
            TasksInProject = new ObservableCollection<TaskInProject>(taskInProjectDao.SearchByProjectID(projectID));
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
        
        public void ShowEmployeeInProject(string  projectID)
        {
            LoadTaskInProjects();       
        }

        public void ShowWithID(string projectID)
        {
            TasksInProjectViewModel.projectID = projectID;
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
        void ShowEmployeeInProject(string projectID);
    }
}
