using System;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Dialogs;
using CompanyManagement.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel : BaseViewModel, IShowTasksInProject
    {

        private ObservableCollection<TaskInProject> tasksInProject;
        public ObservableCollection<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private ObservableCollection<Employee> employeesInProject;
        public ObservableCollection<Employee> EmployeesInProject { get => employeesInProject; set { employeesInProject = value; OnPropertyChanged(); } }

        private string projectID;
        public string ProjectID { get => projectID; set { projectID = value; OnPropertyChanged(); } }

        private string createBy = SingletonAccount.Instance.CurrentAccount.EmployeeId;
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();
        
        public TasksInProjectViewModel()
        {
            id = AutoGenerateID();
            LoadTaskInProjects();
            SetCommands();
            
        }

        public TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(ID, CreateBy, ProjectID);
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
        public void ShowEmployeeInProject(string  projectID)
        {
            EmployeesInProject= new ObservableCollection<Employee>(projectAssignmentDao.GetEmployeesInProject(projectID));       
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

    public interface IShowTasksInProject
    {
        void Add(TaskInProject task);
        void Update(TaskInProject task);   
        void ShowWithID(string projectID);
        void ShowEmployeeInProject(string projectID);
    }
}
