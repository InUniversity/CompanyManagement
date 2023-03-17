using CompanyManagement.Database;
using CompanyManagement.Models;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class TasksInProjectViewModel: BaseViewModel
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
            SetCommand();
            LoadTask();
        }
        private void SetCommand()
        {
            
        }
        private void LoadTask()
        {
            TasksInProject = new ObservableCollection<TaskInProject>();
            DataTable dataTable = taskInProjectDao.GetDataTable(ProjectID);
            foreach (DataRow row in dataTable.Rows)
            {
                TaskInProject task = new TaskInProject(row);
                TasksInProject.Add(task);
            }
        }

        private void Add(TaskInProject task)
        {
            
        }
        
        private void Delete(string id)
        {
            
        }

        private void Update(TaskInProject task)
        {
           
        }
    }
}
