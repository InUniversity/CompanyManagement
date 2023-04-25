using System;
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

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjects
    {
        List<Project> Projects { get; set; }
        Project SelectedProject { get; set; }
        Visibility VisibleAddButton { get; set; }
        Visibility VisibleDeleteButton { get; set; }
        Visibility VisibleUpdateButton { get; set; }
        ICommand OpenProjectInputCommand { get; }
        ICommand DeleteProjectCommand { get; }
        ICommand UpdateProjectCommand { get; }
        ICommand ItemClickCommand { get; }
        IProjectsStrategy ProjectsStrategy { set; }
        INavigateAssignmentView ParentDataContext { set; }
        IRetrieveProjectID ProjectDetailsDataContext { set; }
    }
    
    public class ProjectsViewModel : BaseViewModel, IProjects
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

        public ICommand OpenProjectInputCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand ItemClickCommand { get; set; }

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

        private ProjectDao projectDao = new ProjectDao();
        private ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();
        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        private List<Department> departmentsBeforeChange;

        public ProjectsViewModel(IProjectsStrategy projectsStrategy)
        {
            ProjectsStrategy = projectsStrategy;
            SetCommands();
        }

        private void LoadProjects()
        {
            Projects = projectsStrategy.GetProjects(currentEmployee.ID);

            var listOngoingProjects = Projects.Where(p => p.Progress != BaseDao.COMPLETED && p.End > DateTime.Now).ToList();
            OngoingProjects = new List<Project>(listOngoingProjects);

            var listCompletedProjects = Projects.Where(p => p.Progress == BaseDao.COMPLETED).ToList();
            CompletedProjects = new List<Project>(listCompletedProjects);

            var listOverdueProjects = Projects.Where(p => p.End < DateTime.Now && p.Progress != BaseDao.COMPLETED).ToList();
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
            return new Project(AutoGenerateID(), "", DateTime.Now, DateTime.Now, 
                Utils.EMPTY_DATETIME, "0", "", CurrentUser.Ins.EmployeeIns.ID, 0,
                new ObservableCollection<Department>());
        }

        private void Add(Project project)
        {
            projectDao.Add(project);
            foreach (var department in project.Departments)
            {
                projectAssignmentDao.Add(new ProjectAssignment(project.ID, department.ID));
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
            } while (projectDao.SearchByID(projectID) != null);
            return projectID;
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService(
                "Xóa dự án",
                "Bạn chắc chắn muỗn xóa dự án này ?",
                () =>
                { 
                    projectDao.Delete(id); 
                    LoadProjects();
                }, null);
            dialog.Show();
        }

        private void OpenUpdateProjectDialog(Project project)
        {
            departmentsBeforeChange = projectAssignmentDao.GetAllDepartmentInProject(project.ID);
            project.Departments = new ObservableCollection<Department>(departmentsBeforeChange);
            var inputService = new InputDialogService<Project>(new UpdateProjectDialog(), project, Update);
            inputService.Show();
        }

        private void Update(Project project)
        {
            projectDao.Update(project);
            UpdateProjectAssignment(project.ID, project.Departments);
            LoadProjects();
        }

        private void UpdateProjectAssignment(string projectID, ICollection<Department> departmentsAfterChange)
        {
            var deletedDepartments = departmentsBeforeChange.Except(departmentsAfterChange);
            foreach (var department in deletedDepartments)
            {
                projectAssignmentDao.Delete(new ProjectAssignment(projectID, department.ID));
            }
            var addedDepartments = departmentsAfterChange.Except(departmentsBeforeChange);
            foreach (var department in addedDepartments)
            {
                projectAssignmentDao.Add(new ProjectAssignment(projectID, department.ID));
            }
        }

        private void ItemClicked(object obj)
        {
            if (SelectedProject == null) return;
            ProjectDetailsDataContext.RetrieveProjectID(SelectedProject.ID);
            ParentDataContext.MoveToProjectDetailsView();
            SelectedProject = null;
        }
    }
}