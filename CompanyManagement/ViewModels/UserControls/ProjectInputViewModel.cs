using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.Database;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjectInput
    {
        Project CreateProjectInstance();
        bool CheckAllFields();
        void TrimAllTexts();
        void Receive(Project project);
    }

    public class ProjectInputViewModel : BaseViewModel, IProjectInput
    {
        private Project project = new Project();
        public string ID { get => project.ID; set { project.ID = value; OnPropertyChanged(); } }
        public string Name { get => project.Name; set { project.Name = value; OnPropertyChanged(); } }
        public DateTime Start { get => project.Start; set { project.Start = value; OnPropertyChanged(); LoadDepartmentsCanAssign(); } }
        public DateTime End { get => project.End; set { project.End = value; OnPropertyChanged(); LoadDepartmentsCanAssign(); } }
        public DateTime Completed { get => project.Completed; set { project.Completed = value; OnPropertyChanged(); } }
        public string Progress { get => project.Progress; set { project.Progress = value; OnPropertyChanged(); } }
        public string ProjectStatusID { get => project.StatusID; set { project.StatusID = value; OnPropertyChanged(); } }
        public string CreateBy { get => project.CreateBy; set { project.CreateBy = value; OnPropertyChanged(); } }
        public ObservableCollection<Department> DepartmentsInProject 
        { get => new ObservableCollection<Department>(project.Departments) ; set { project.Departments = value; OnPropertyChanged(); } }

        private List<Department> departmentsCanAssign;
        
        private ObservableCollection<Department> searchedDepartmentsCanAssign;
        public ObservableCollection<Department> SearchedDepartmentsCanAssign 
        { get => searchedDepartmentsCanAssign; set { searchedDepartmentsCanAssign = value; OnPropertyChanged();} }

        private List<Department> selectedDepartments;
        public List<Department> SelectedDepartments 
        { get => selectedDepartments; set { selectedDepartments = value; OnPropertyChanged();} }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }
        
        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        public ICommand AddDepartmentCommand { get; set; }
        public ICommand DeleteDepartmentCommand { get; set; }
        public ICommand GetAllSelectedDepartmentCommand { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        private ProjectStatusDao projectStatusDao = new ProjectStatusDao();
        private ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();

        public ProjectInputViewModel()
        {
            SetCommands();
            SetAllComboBox();
        }

        private void LoadDepartmentsInProject()
        {
            var departments = projectAssignmentDao.GetAllDepartmentInProject(project.ID);
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        private void LoadDepartmentsCanAssign()
        {
            departmentsCanAssign = projectAssignmentDao.GetDepartmentsCanAssignWork(project.ID, 
                Utils.ToFormatSQLServer(project.Start), Utils.ToFormatSQLServer(project.End));
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(departmentsCanAssign);
            
            Log.Instance.Information(nameof(ProjectInputViewModel), $"Start:{Start}, End:{End}");
            Log.Instance.Information(nameof(ProjectInputViewModel), $"{project.ID}");
            Log.Instance.Information(nameof(ProjectInputViewModel), $"{departmentsCanAssign.Count}");
        }

        private void SetCommands()
        {
            GetAllSelectedDepartmentCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedDepartment);
            AddDepartmentCommand = new RelayCommand<object>(ExecuteAddDepartmentCommand);
            DeleteDepartmentCommand = new RelayCommand<Department>(ExecuteDeleteDepartmentCommand);
        }

        private void SetAllComboBox()
        {
            ProjectStatuses = projectStatusDao.GetAll();
        }
        
        private void ExecuteGetAllSelectedDepartment(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<Department>().ToList();
            SelectedDepartments = selectedItems;
        }

        private void ExecuteAddDepartmentCommand(object obj)
        {
            if (SelectedDepartments != null)
            {
                foreach (var department in SelectedDepartments)
                {
                    DepartmentsInProject.Add(department);
                    departmentsCanAssign.Remove(department);
                    SearchedDepartmentsCanAssign.Remove(department);
                }
            }                
            SelectedDepartments = null;
        }

        private void ExecuteDeleteDepartmentCommand(Department department)
        {
            DepartmentsInProject.Remove(department);
            departmentsCanAssign.Add(department);
            SearchedDepartmentsCanAssign.Add(department);
        }

        private void SearchByName()
        {
            var searchedItems = departmentsCanAssign;
            if (!string.IsNullOrWhiteSpace(textToSearch))
            {
                searchedItems = departmentsCanAssign
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(searchedItems);
        }

        public Project CreateProjectInstance()
        {
            return new Project(ID, Name, Start, End, Completed, Progress, ProjectStatusID, CreateBy, DepartmentsInProject);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = Utils.INVALIDATE_EMPTY_MESSAGE;
                return false;
            }
            if (CheckFormat.ValidateTimeline(Start, End))
            {
                ErrorMessage = Utils.INVALIDATE_TIMELINE;
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            Name = Name.Trim();
        }

        public void Receive(Project project)
        {
            this.project = project;
            LoadDepartmentsCanAssign();
        }
    }
}