using System;
using System.Collections.Generic;
using System.Windows.Input;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Utilities;
using ProjectAssignmentDao = CompanyManagement.Database.ProjectAssignmentDao;
using ProjectDao = CompanyManagement.Database.ProjectDao;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjects
    {
        INavigateAssignmentView ParentDataContext { set; }
        IRetrieveProjectID ProjectDetailsDataContext { set; }
    }
    
    public class ProjectsViewModel : BaseViewModel, IProjects, IEditDBViewModel
    {

        private List<Project> projects;
        public List<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); } }

        private Project selectedProject;
        public Project SelectedProject { get => selectedProject; set { selectedProject = value; OnPropertyChanged(); } }
        
        public ICommand OpenProjectInputCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

        public INavigateAssignmentView ParentDataContext { get; set; }
        public IRetrieveProjectID ProjectDetailsDataContext { get; set; }

        private ProjectDao projectDao;
        private ProjectAssignmentDao projectAssignmentDao;

        public ProjectsViewModel()
        {
            projectDao = new ProjectDao();
            projectAssignmentDao = new ProjectAssignmentDao();
            LoadProjects();
            SetCommands();
        }

        private void LoadProjects()
        {
            var employeeID = CurrentUser.Instance.CurrentAccount.EmployeeID;
            Projects = projectAssignmentDao.SearchProjectByEmployeeID(employeeID);;
        }

        private void SetCommands()
        {
            OpenProjectInputCommand = new RelayCommand<object>(OpenAddProjectDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<Project>(OpenUpdateProjectDialog);
            ItemClickCommand = new RelayCommand<object>(ItemClicked);
        }

        public void AddToDB(object project)
        {
            projectDao.Add(project as Project);
            LoadProjects();
        }

        public void UpdateToDB(object project)
        {
            projectDao.Update(project as Project);
            LoadProjects();
        }

        private void OpenAddProjectDialog(object obj)
        {
            AddProjectDialog addProjectDialog = new AddProjectDialog();
            IDialogViewModel addProjectVM = (IDialogViewModel)addProjectDialog.DataContext;
            addProjectVM.ParentDataContext = this;
            Project project = CreateProject();
            addProjectVM.Retrieve(project);
            addProjectDialog.ShowDialog();
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
            projectDao.Delete(id);
            LoadProjects();
        }

        private void OpenUpdateProjectDialog(Project project)
        {
            UpdateProjectDialog projectDetailsDialog = new UpdateProjectDialog();
            IDialogViewModel projectViewModel = (IDialogViewModel)projectDetailsDialog.DataContext;
            projectViewModel.ParentDataContext = this;
            projectViewModel.Retrieve(project);
            projectDetailsDialog.ShowDialog();
        }

        private void ItemClicked(object obj)
        {
            ProjectDetailsDataContext.RetrieveProjectID(SelectedProject.ID);
            ParentDataContext.MoveToProjectDetailsView();
        }
    }
}