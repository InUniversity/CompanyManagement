using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Utilities;
using CompanyManagement.Database;
using CompanyManagement.Services;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjects
    {
        INavigateAssignmentView ParentDataContext { set; }
        IRetrieveProjectID ProjectDetailsDataContext { set; }
    }
    
    public class ProjectsViewModel : BaseViewModel, IProjects
    {

        private List<Project> projects;
        public List<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); } }

        private Project selectedProject;
        public Project SelectedProject { get => selectedProject; set { selectedProject = value; OnPropertyChanged(); } }

        private Visibility visibleAddButton = Visibility.Collapsed;
        public Visibility VisibleAddButton { get => visibleAddButton; set { visibleAddButton = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        private Visibility visibleUpdateButton = Visibility.Collapsed;
        public Visibility VisibleUpdateButton { get => visibleUpdateButton; set { visibleUpdateButton = value; OnPropertyChanged(); } }

        public ICommand OpenProjectInputCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

        public INavigateAssignmentView ParentDataContext { get; set; }
        public IRetrieveProjectID ProjectDetailsDataContext { get; set; }

        private ProjectDao projectDao;
        private ProjectAssignmentDao projectAssignmentDao;
        private string currentEmployeeID = CurrentUser.Instance.CurrentEmployee.ID;

        public ProjectsViewModel()
        {
            projectDao = new ProjectDao();
            projectAssignmentDao = new ProjectAssignmentDao();
            LoadProjects();
            SetVisible();
            SetCommands();
        }

        private void LoadProjects()
        {
            List<Project> projects = CurrentUser.Instance.IsEmployee()
                ? projectAssignmentDao.SearchProjectByEmployeeID(currentEmployeeID)
                : projectAssignmentDao.SearchProjectByCreatorID(currentEmployeeID);
            Projects = projects;
        }

        private void SetVisible()
        {
            if (!CurrentUser.Instance.IsEmployee())
            {
                VisibilityCRUD();
                VisibilityCRUDCommands();
            }
        }

        private void VisibilityCRUD()
        {
            visibleAddButton = Visibility.Visible;
            visibleDeleteButton = Visibility.Visible;
            visibleUpdateButton = Visibility.Visible;
        }

        private void VisibilityCRUDCommands()
        {
            OpenProjectInputCommand = new RelayCommand<object>(OpenAddProjectDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<Project>(OpenUpdateProjectDialog);
        }

        private void SetCommands()
        {
            ItemClickCommand = new RelayCommand<object>(ItemClicked);
        }

        private void OpenAddProjectDialog(object obj)
        {
            Project project = CreateProject();
            var inputService = new InputDialogService<Project>(new AddProjectDialog(), project, Add);
            inputService.Show();
        }

        private void Add(Project project)
        {
            projectDao.Add(project);
            LoadProjects();
        }

        private Project CreateProject()
        {
            return new Project(AutoGenerateID(), "", DateTime.Now, DateTime.Now, 
                Utils.EMPTY_DATETIME, "0", "", CurrentUser.Instance.CurrentAccount.EmployeeID);
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
            AlertDialogService dialog = new AlertDialogService(
             "Xóa dự án",
             "Bạn chắc chắn muốn xóa dự án !",
             () =>
             {
                 projectDao.Delete(id); 
                 LoadProjects();
             }, () => { });
            dialog.Show();
        }

        private void OpenUpdateProjectDialog(Project project)
        {
            var inputService = new InputDialogService<Project>(new UpdateProjectDialog(), project, Update);
            inputService.Show();
        }

        private void Update(Project project)
        {
            projectDao.Update(project);
            LoadProjects();
        }

        private void ItemClicked(object obj)
        {
            if (selectedProject != null) 
            {
                ProjectDetailsDataContext.RetrieveProjectID(SelectedProject.ID);
                ParentDataContext.MoveToProjectDetailsView();
            }
        }
    }
}