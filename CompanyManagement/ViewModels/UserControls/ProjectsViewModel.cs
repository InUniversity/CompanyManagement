using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjects
    {
        void Add(Project project);
        void Update(Project project);
    }
    
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

        private IProjectDao projectDao;

        public ProjectsViewModel(IProjectDao projectDao)
        {
            this.projectDao = projectDao;
            LoadProjects();
            SetCommands();
        }

        private void LoadProjects()
        {
            Projects = new ObservableCollection<Project>(projectDao.GetAll());
        }

        private void SetCommands()
        {
            OpenProjectInputCommand = new RelayCommand<object>(OpenAddProjectDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<Project>(OpenUpdateProjectDialog);
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

        private void OpenAddProjectDialog(object p)
        {
            AddProjectDialog addProjectDialog = new AddProjectDialog();
            AddProjectViewModel addProjectVM = (AddProjectViewModel)addProjectDialog.DataContext;
            addProjectVM.ParentDataContext = this;
            addProjectVM.ProjectInputDataContext.RetrieveProject(new Project(AutoGenerateID()));
            addProjectDialog.ShowDialog();
        }

        private string AutoGenerateID()
        {
            string projectID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                projectID = $"PRJ{number:0000}";
            } while (projectDao.SearchByID(projectID) != null);
            return projectID;
        }

        private void ExecuteDeleteCommand(string id)
        {
            projectDao.Delete(id);
            LoadProjects();
        }

        private void OpenUpdateProjectDialog(Project project)
        {
            UpdateProjectDialog projectDetailsDialog = new UpdateProjectDialog();
            UpdateProjectViewModel projectViewModel = (UpdateProjectViewModel)projectDetailsDialog.DataContext;
            projectViewModel.ParentDataContext = this;
            projectViewModel.ProjectInputDataContext.RetrieveProject(project);
            projectDetailsDialog.ShowDialog();
        }

        private void ItemClicked(object p)
        {
            TasksDataContext.ShowTasksWithID(SelectedProject.ID);
        }
    }
}
