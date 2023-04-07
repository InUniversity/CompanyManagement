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
        void RetrieveProject(Project project);
    }

    public class ProjectInputViewModel : BaseViewModel, IProjectInput
    {

        private string id = "";
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }

        private string name = "";
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }

        private DateTime start = DateTime.Now;
        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }

        private DateTime end = DateTime.Now;
        public DateTime End { get => end; set { end = value; OnPropertyChanged(); } }

        private DateTime completed = DateTime.Now;
        public DateTime Completed { get => completed; set { completed = value; OnPropertyChanged(); } }

        private string progress = "0";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string projectStatusID = "1";
        public string ProjectStatusID { get => projectStatusID; set { projectStatusID = value; OnPropertyChanged(); } }

        private string createBy = CurrentUser.Instance.CurrentAccount.EmployeeID;
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsInProject;
        public ObservableCollection<Department> DepartmentsInProject { get => departmentsInProject; set => departmentsInProject = value; }

        private List<Department> departmentsCanAssign;
        
        private ObservableCollection<Department> searchedDepartmentsCanAssign;
        public ObservableCollection<Department> SearchedDepartmentsCanAssign { get => searchedDepartmentsCanAssign; set => searchedDepartmentsCanAssign = value; }

        private List<Department> departmentsIsSelected;
        public List<Department> DepartmentsIsSelected { get => departmentsIsSelected; set => departmentsIsSelected = value; }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand AddDepartmentCommand { get; set; }
        public ICommand DeleteDepartmentCommand { get; set; }
        public ICommand GetAllSelectedDepartmentCommand { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        private ProjectStatusDao projectStatusDao;
        private ProjectAssignmentDao projectAssignmentDao;

        public ProjectInputViewModel()
        {
            projectStatusDao = new ProjectStatusDao();
            projectAssignmentDao = new ProjectAssignmentDao();
            SetCommands();
            SetAllComboBox();
        }

        private void LoadDepartmentsInProject(string projectID)
        {
            var departments = projectAssignmentDao.GetAllDepartmentInProject(projectID);
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        private void LoadDepartmentsCanAssign(Project project)
        {
            departmentsCanAssign = projectAssignmentDao.GetDepartmentsCanAssignWork(project);
            Log.Instance.Information(nameof(ProjectInputViewModel), $"{project.ID}");
            Log.Instance.Information(nameof(ProjectInputViewModel), $"{departmentsCanAssign.Count.ToString()}");
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(departmentsCanAssign);
        }

        private void SetCommands()
        {
            GetAllSelectedDepartmentCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedDepartment);
            AddDepartmentCommand = new RelayCommand<object>(ExecuteAddDepartmentCommand);
            DeleteDepartmentCommand = new RelayCommand<string>(ExecuteDeleteDepartmentCommand);
        }

        private void SetAllComboBox()
        {
            ProjectStatuses = projectStatusDao.GetAll();
        }
        
        private void ExecuteGetAllSelectedDepartment(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<Department>().ToList();
            DepartmentsIsSelected = selectedItems;
        }

        private void ExecuteAddDepartmentCommand(object b)
        {
            if(DepartmentsIsSelected != null)
            {
                foreach (Department department in DepartmentsIsSelected)
                {
                    projectAssignmentDao.Add(new ProjectAssignment(ID, department.ID));
                }
                LoadDepartmentsInProject(ID);
                LoadDepartmentsCanAssign(CreateProjectInstance());
            }                
            DepartmentsIsSelected = new List<Department>();
        }

        private void ExecuteDeleteDepartmentCommand(string departmentID)
        {
            projectAssignmentDao.Delete(new ProjectAssignment(ID, departmentID));
            LoadDepartmentsInProject(ID);
            LoadDepartmentsCanAssign(CreateProjectInstance());
        }

        private void SearchByName()
        {
            var searchedItems = departmentsCanAssign;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = departmentsCanAssign
                    .Where(item => item.Name.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(searchedItems);
        }

        public Project CreateProjectInstance()
        {
            return new Project(id, name, start, end, completed, progress, projectStatusID, createBy);
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
            ID = id.Trim();
            Name = name.Trim();
            Progress = progress.Trim();
        }

        public void RetrieveProject(Project project)
        {
            ID = project.ID;
            Name = project.Name;
            Start = project.Start;
            End = project.End;
            Completed = project.Completed;
            Progress = project.Progress;
            ProjectStatusID = project.StatusID;
            LoadDepartmentsInProject(project.ID);
            LoadDepartmentsCanAssign(project);
        }
    }
}