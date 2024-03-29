﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Utilities;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Strategies.UserControls.ProjectsView;
using CompanyManagement.Database.Base;
using CompanyManagement.Enums;

namespace CompanyManagement.ViewModels.UserControls
{
    public class ProjectsViewModel : BaseViewModel
    {
        private List<Project> projects;
        public List<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); } }

        private List<Project> ongoingProjects;
        public List<Project> OngoingProjects { get => ongoingProjects; set { ongoingProjects = value; OnPropertyChanged(); } }

        private List<Project> completedProjects;
        public List<Project> CompletedProjects { get => completedProjects; set { completedProjects = value; OnPropertyChanged(); } }

        private List<Project> overdueProjects;
        public List<Project> OverdueProjects { get => overdueProjects; set { overdueProjects = value; OnPropertyChanged(); } }

        private Project selectedProject;
        public Project SelectedProject { get => selectedProject; set { selectedProject = value; OnPropertyChanged(); } }

        private Visibility visibleAddButton = Visibility.Collapsed;
        public Visibility VisibleAddButton { get => visibleAddButton; set { visibleAddButton = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        private Visibility visibleUpdateButton = Visibility.Collapsed;
        public Visibility VisibleUpdateButton { get => visibleUpdateButton; set { visibleUpdateButton = value; OnPropertyChanged(); } }

        public ICommand OpenProjectInputCommand { get; private set; }
        public ICommand DeleteProjectCommand { get; private set; }
        public ICommand UpdateProjectCommand { get; private set; }
        public ICommand ItemClickCommand { get; private set; }

        private IProjectsStrategy projectsStrategy;
        public IProjectsStrategy ProjectsStrategy 
        { 
            get => projectsStrategy;
            set
            {
                if (projectsStrategy == value) return;
                projectsStrategy = value;
                projectsStrategy.SetVisible(this);
                LoadProjects();
            }
        }

        public INavigateAssignmentView ParentDataContext { get; set; }
        public IRetrieveProjectID ProjectDetailsDataContext { get; set; }

        private ProjectsDao projectsDao = new ProjectsDao();
        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();
        private TasksDao tasksDao = new TasksDao();
        private ProjectBonusesDao projBonusDao = new ProjectBonusesDao();
        private MilestonesDao milestonesDao = new MilestonesDao();
        private Employee currentEmployee = CurrentUser.Ins.Empl;

        private List<Department> departmentsBeforeChange;

        public ProjectsViewModel(IProjectsStrategy projectsStrategy)
        {
            ProjectsStrategy = projectsStrategy;
            SetCommands();
        }

        private void LoadProjects()
        {
            Projects = projectsStrategy.GetProjects(currentEmployee.ID);

            var listOngoingProjects = Projects.Where(p => p.Progress != BaseDao.completed && p.EndDate > DateTime.Now).ToList();
            OngoingProjects = new List<Project>(listOngoingProjects);

            var listCompletedProjects = Projects.Where(p => p.Progress == BaseDao.completed).ToList();
            CompletedProjects = new List<Project>(listCompletedProjects);

            var listOverdueProjects = Projects.Where(p => p.EndDate < DateTime.Now && p.Progress != BaseDao.completed).ToList();
            OverdueProjects = new List<Project>(listOverdueProjects);
        }

        private void SetCommands()
        {
            ItemClickCommand = new RelayCommand<object>(ItemClicked);
            OpenProjectInputCommand = new RelayCommand<object>(OpenAddProjectDialog);
            DeleteProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<Project>(OpenUpdateProjectDialog);
        }

        private void OpenAddProjectDialog(object obj)
        {
            var project = CreateProject();
            var inputService = new InputDialogService<Project>(new AddProjectDialog(), project, Add);
            inputService.Show();
        }

        private Project CreateProject()
        {
            return new Project(AutoGenerateID(), "", "", DateTime.Now, DateTime.Now, 
                DateTime.Now, Utils.emptyDate, "0", EProjStatus.Planning, 
                currentEmployee.ID, 0, new ObservableCollection<Department>());
        }

        private void Add(Project project)
        {
            projectsDao.Add(project);
            foreach (var department in project.Departments)
            {
                assignmentsDao.Add(new ProjectAssignment(project.ID, department.ID));
            }
            LoadProjects();
        }

        private string AutoGenerateID()
        {
            string projectID;
            Random random = new Random();
            do
            {
                int number = random.Next(10000);
                projectID = $"PRJ{number:0000}";
            } while (projectsDao.SearchByID(projectID) != null);
            return projectID;
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService(
                "Xóa dự án",
                "Bạn chắc chắn muỗn xóa dự án này ?",
                () =>
                { 
                    projectsDao.Delete(id);
                    DeleteProjectDependencies(id);
                    LoadProjects();
                }, null);
            dialog.Show();
        }

        private void DeleteProjectDependencies(string projID)
        {
            assignmentsDao.DeleteByProjID(projID);
            tasksDao.DeleteByProjID(projID);
            projBonusDao.DeleteProjID(projID);
            milestonesDao.DeleteProjID(projID);
        }

        private void OpenUpdateProjectDialog(Project project)
        {
            departmentsBeforeChange = assignmentsDao.GetAllDepartmentInProject(project.ID);
            project.Departments = new ObservableCollection<Department>(departmentsBeforeChange);
            var inputService = new InputDialogService<Project>(new UpdateProjectDialog(), project, Update);
            inputService.Show();
        }

        private void Update(Project project)
        {
            projectsDao.Update(project);
            UpdateProjectAssignment(project.ID, project.Departments);
            LoadProjects();
        }

        private void UpdateProjectAssignment(string projectID, ICollection<Department> departmentsAfterChange)
        {
            var deletedDepartments = departmentsBeforeChange.Except(departmentsAfterChange);
            foreach (var department in deletedDepartments)
            {
                assignmentsDao.Delete(new ProjectAssignment(projectID, department.ID));
            }
            var addedDepartments = departmentsAfterChange.Except(departmentsBeforeChange);
            foreach (var department in addedDepartments)
            {
                assignmentsDao.Add(new ProjectAssignment(projectID, department.ID));
            }
        }

        private void ItemClicked(object obj)
        {
            if (SelectedProject == null) return;
            ProjectDetailsDataContext.ReceiveProjectID(SelectedProject.ID);
            ParentDataContext.MoveToProjectDetailsView();
            SelectedProject = null;
        }
    }
}