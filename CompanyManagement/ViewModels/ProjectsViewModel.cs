using CompanyManagement.Database;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using CompanyManagement.Dialogs;
using System.Windows;

namespace CompanyManagement.ViewModels
{
    public class ProjectsViewModel : BaseViewModel, IProjects
    {

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); } }

        public Project SelectedProject { get; set; }

        public ICommand OpenProjectInputCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

        public TasksInProjectViewModel TasksDataContext { get; set; }

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
            OpenProjectInputCommand = new RelayCommand<object>(OpenProjectInputDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<Project>(ExecuteUpdateProjectDialog) ;
            ItemClickCommand = new RelayCommand<object>(ItemClicked);
        }

        public void Add(Project project)
        {          
            projectDao.Add(project);
            LoadProjects();
        }

        public void Update(Project project)
        {
            projectDao.Update(project);
            LoadProjects();
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

        private void ExecuteUpdateProjectDialog(Project project)
        {
            UpdateProjectDialog projectDetailsDialog = new UpdateProjectDialog();
            UpdateProjectViewModel projectViewModel = (UpdateProjectViewModel)projectDetailsDialog.DataContext;
            projectViewModel.ParentDataContext = this;
            projectViewModel.ProjectInputDataContext.Retrieve(project);
            projectDetailsDialog.ShowDialog();
        }

        private void ItemClicked(object p)
        {
            TasksDataContext.ShowWithID(SelectedProject.ID);
        }
    }

    public interface IProjects
    {
        void Add(Project project);
        void Update(Project project);
    }
}
