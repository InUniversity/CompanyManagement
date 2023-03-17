using CompanyManagement.Database;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using CompanyManagement.Dialogs;

namespace CompanyManagement.ViewModels
{
    public class ProjectsViewModel : BaseViewModel,IProjects
    {      

        private ObservableCollection<Project> projects;

        public ObservableCollection<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); }}

        public ICommand UpdateProjectCommand { get; set; }

        public ICommand OpenProjectInputCommand { get; set; }

        public ICommand DeleteProjectCommand { get; set; }

        private ProjectDao projectDao = new ProjectDao();

        public ProjectsViewModel()
        {
            LoadProjects();
            SetCommands();
        }

        private void LoadProjects()
        {
            Projects = new ObservableCollection<Project>();
            DataTable dataTable = projectDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Project project = new Project(row);
                Projects.Add(project);
            }
        }

        private void SetCommands()
        {
            UpdateProjectCommand = new RelayCommand<Project>(ExecuteUpdateProjectDialog) ;
            OpenProjectInputCommand = new RelayCommand<object>(OpenProjectInputDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
        }

        public void Add(Project project)
        {          
            projectDao.Add(project);
            LoadProjects();
        }

        public void Delete(string id)
        {
            projectDao.Delete(id);
            LoadProjects();
        }

        public void Update(Project project)
        {
            projectDao.Save(project);
            LoadProjects();
        }

        private void ExecuteUpdateProjectDialog(Project project)
        {
            UpdateProjectDialog projectDetailsDialog = new UpdateProjectDialog();
            UpdateProjectViewModel projectViewModel = (UpdateProjectViewModel)projectDetailsDialog.DataContext;
            projectViewModel.ParentDataContext = this;
            projectViewModel.ProjectInputDataContext.Retrieve(project);
            projectDetailsDialog.ShowDialog();
        }

        private void OpenProjectInputDialog(object p)
        {
            AddProjectDialog addProjectDialog = new AddProjectDialog();
            AddProjectViewModel addProjectVM = (AddProjectViewModel)addProjectDialog.DataContext;
            addProjectVM.ParentDataContext = this;
            addProjectDialog.ShowDialog();
        }

        private void ExecuteDeleteCommand(string id)
        {
            projectDao.Delete(id);
            LoadProjects();
        }
    }

    public interface IProjects
    {
        void Add(Project project);

        void Update(Project project);
    }
}
