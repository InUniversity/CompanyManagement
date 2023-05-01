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
    public class ProjectInputViewModel : BaseViewModel
    {
        private Project project = new Project();
        public Project ProjectIns { get => project; set => project = value; }

        public string ID { get => project.ID; set { project.ID = value; OnPropertyChanged(); } }
        public string Name { get => project.Name; set { project.Name = value; OnPropertyChanged(); } }
        public string Details { get => project.Details; set { project.Details = value; OnPropertyChanged(); } }
        public DateTime CreatedDate { get => project.CreatedDate; set { project.CreatedDate = value; OnPropertyChanged(); } }
        public DateTime StartDate 
        { get => project.StartDate; set { project.StartDate = value; OnPropertyChanged(); LoadDepartmentsCanAssign(); } }
        public DateTime EndDate 
        { get => project.EndDate; set { project.EndDate = value; OnPropertyChanged(); LoadDepartmentsCanAssign(); } }
        public string Progress { get => project.Progress; set { project.Progress = value; OnPropertyChanged(); } }
        public string StatusID { get => project.StatusID; set { project.StatusID = value; OnPropertyChanged(); } }
        public string OwnerID { get => project.OwnerID; set { project.OwnerID = value; OnPropertyChanged(); } }
        public decimal BonusSalary { get => project.BonusSalary; set { project.BonusSalary = value; OnPropertyChanged(); } }
        public ObservableCollection<Department> DepartmentsInProject 
        { get => project.Departments; set { project.Departments = value; OnPropertyChanged(); } }

        private List<Department> departmentsCanAssign = new();
        
        private ObservableCollection<Department> searchedDepartmentsCanAssign = new();
        public ObservableCollection<Department> SearchedDepartmentsCanAssign 
        { get => searchedDepartmentsCanAssign; set { searchedDepartmentsCanAssign = value; OnPropertyChanged();} }

        private List<Department> selectedDepartments = new();
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

        private ProjectStatusesDao projectStatusesDao = new ProjectStatusesDao();
        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();
        private CheckFormat checker = new CheckFormat();

        public ProjectInputViewModel()
        {
            SetCommands();
            SetAllComboBox();
        }

        private void LoadDepartmentsInProject()
        {
            var departments = assignmentsDao.GetAllDepartmentInProject(project.ID);
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        private void LoadDepartmentsCanAssign()
        {
            departmentsCanAssign = assignmentsDao.GetDepartmentsCanAssignWork(project.ID, 
                Utils.ToSQLFormat(project.StartDate), Utils.ToSQLFormat(project.EndDate));
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(departmentsCanAssign);
            
            Log.Instance.Information(nameof(ProjectInputViewModel), $"Start:{StartDate}, End:{EndDate}");
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
            ProjectStatuses = projectStatusesDao.GetAll();
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

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = Utils.invalidEmptyMess;
                return false;
            }
            if (!checker.ValidateTimeline(StartDate, EndDate))
            {
                ErrorMessage = Utils.invalidTimeline;
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
        {
            Name = Name.Trim();
        }
    }
}