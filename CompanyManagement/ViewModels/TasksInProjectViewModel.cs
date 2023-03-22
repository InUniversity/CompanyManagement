using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.UserControls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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

        ICommand OpenTaskInProjectInoutCommand { get; set; }
        ICommand DeleteTaskInProjectCommand { get; set; }
        ICommand UpdateTaskInProjectCommand { get; set; }

        TaskInProjectDao taskInProjectDao  = new TaskInProjectDao();
        ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();
        
        public TasksInProjectViewModel()
        {
            SetCommands();
            
        }

        private void SetCommands()
        {
            
        }
        public void ShowEmployeeInProject(string  projectID)
        {
            EmployeesInProject= new ObservableCollection<Employee>(projectAssignmentDao.GetEmployeesInProject(projectID));       
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
        void ShowEmployeeInProject(string projectID);
    }
}
