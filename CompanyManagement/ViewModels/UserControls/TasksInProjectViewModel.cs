using System;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Base;

namespace CompanyManagement.ViewModels.UserControls
{
    
    public interface ITasksInProject
    {
        void Add(TaskInProject task);
        void Update(TaskInProject task);
    }

    public interface IRetrieveProjectID
    {
        void RetrieveProjectID(string projectID);
    }
    
    public class TasksInProjectViewModel : BaseViewModel, ITasksInProject, IRetrieveProjectID
    {

        private ObservableCollection<TaskInProject> tasksInProject;
        public ObservableCollection<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> employeesInProject;
        public ObservableCollection<Employee> EmployeesInProject { get => employeesInProject; set { employeesInProject = value; OnPropertyChanged(); } }

        public static string projectID = "";

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        private ITaskInProjectDao taskInProjectDao;
        private IProjectAssignmentDao projectAssignmentDao;
        private ITaskStatusDao taskStatusDao;

        public TasksInProjectViewModel(ITaskInProjectDao taskInProjectDao, IProjectAssignmentDao projectAssignmentDao, ITaskStatusDao taskStatusDao)
        {
            this.taskInProjectDao = taskInProjectDao;
            this.projectAssignmentDao = projectAssignmentDao;
            this.taskStatusDao = taskStatusDao;
            LoadTaskInProjects();
            SetCommands();
        }

        private TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), "", "", "", "", "",
                SingletonEmployee.Instance.CurrentAccount.EmployeeID, "", projectID, "1");
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

        public void RetrieveProjectID(string projectID)
        {
            TasksInProjectViewModel.projectID = projectID;
           // TimeKeepingViewModel.projectID = projectID;
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
}
