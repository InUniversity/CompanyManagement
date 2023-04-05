using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Utilities;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.Views.Windows;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjects
    {
        INavigateAssignmentView ParentDataContext { set; }
        IRetrieveProjectID ProjectDetailsDataContext { set; }
        void Add(Project project);
        void Update(Project project);
    }
    
    public class ProjectsViewModel : BaseViewModel, IProjects
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

        private IProjectDao projectDao;
        private IProjectAssignmentDao projectAssignmentDao;

        public ProjectsViewModel()
        {
            projectDao = new ProjectDao();
            projectAssignmentDao = new ProjectAssignmentDao();
            LoadProjects();
            SetCommands();
        }

        private void LoadProjects()
        {
            var employeeID = SingletonEmployee.Instance.CurrentAccount.EmployeeID;
            Projects = projectAssignmentDao.SearchProjectByEmployeeID(employeeID);;
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
            IAddProject addProjectVM = (IAddProject)addProjectDialog.DataContext;
            addProjectVM.ParentDataContext = this;
            Project project = CreateProject();
            addProjectVM.ProjectInputDataContext.RetrieveProject(project);
            addProjectDialog.ShowDialog();
        }

        private Project CreateProject()
        {
            return new Project(AutoGenerateID(), "", DateTime.Now, DateTime.Now, 
                Utils.EMPTY_DATETIME, "0", "", SingletonEmployee.Instance.CurrentAccount.EmployeeID);
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
            IUpdateProject projectViewModel = (IUpdateProject)projectDetailsDialog.DataContext;
            projectViewModel.ParentDataContext = this;
            projectViewModel.ProjectInputDataContext.RetrieveProject(project);
            projectDetailsDialog.ShowDialog();
        }

        private void ItemClicked(object obj)
        {
            ProjectDetailsDataContext.RetrieveProjectID(SelectedProject.ID);
            ParentDataContext.MoveToProjectDetailsView();
        }
    }
}