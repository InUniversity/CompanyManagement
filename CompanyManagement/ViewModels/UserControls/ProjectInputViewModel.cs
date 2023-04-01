using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using System.Linq;
using System.Windows.Controls;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IProjectInput
    {
        Project CreateProjectInstance();
        bool CheckAllFields();
        void TrimAllTexts();
        void RetrieveProject(Project project);
        void LoadDepartmentsInProject(string projectID);
        void LoadDepartmentsCanAssign(Project project);
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

        private string progress = "0";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string projectStatus = "1";
        public string ProjectStatus { get => projectStatus; set { projectStatus = value; OnPropertyChanged(); } }  

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsInProject;
        public ObservableCollection<Department> DepartmentsInProject { get => departmentsInProject; set { departmentsInProject = value; OnPropertyChanged(); } }

        private List<Department> departmentsCanAssign;

        private ObservableCollection<Department> searchedDepartmentsCanAssign;

        public ObservableCollection<Department> SearchedDepartmentsCanAssign { get => searchedDepartmentsCanAssign; set { searchedDepartmentsCanAssign = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsIsSelected;

        public ObservableCollection<Department> DepartmentsIsSelected { get => departmentsIsSelected; set { departmentsIsSelected = value; OnPropertyChanged(); } }

        private string textToSearch = "";

        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }

        public ICommand AddDepartmentCommand { get; set; }

        public ICommand DeleteDepartmentCommand { get; set; }

        public ICommand GetAllSelectedDepartmentComman { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        private IProjectAssignmentDao projectAssignmentDao;

        private IProjectStatusDao projectStatusDao;
        

        public ProjectInputViewModel(IProjectAssignmentDao projectAssignmentDao, IProjectStatusDao projectStatusDao)
        {
            this.projectStatusDao = projectStatusDao;
            this.projectAssignmentDao = projectAssignmentDao;
            LoadDepartmentsCanAssign(new Project());
            SetCommands();
        }

        void IProjectInput.LoadDepartmentsInProject(string projectID)
        {
            LoadDepartmentsInProject(projectID);
        }       
        private void LoadDepartmentsInProject(string projectID)
        {
            DepartmentsInProject = new ObservableCollection<Department>(projectAssignmentDao.GetAllDepartmentInProject(projectID));
        }

        public void LoadDepartmentsCanAssign(Project project)
        {
            departmentsCanAssign = projectAssignmentDao.GetDepartmentsCanAssignWork(project);
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(departmentsCanAssign);
        }

        private void SetCommands()
        {
            GetAllSelectedDepartmentComman = new RelayCommand<object>(ExecuteGetAllSelectedDepartmentCommnan);
            AddDepartmentCommand = new RelayCommand<object>(ExecuteAddDepartmentCommand);
            DeleteDepartmentCommand = new RelayCommand<string>(ExecuteDeleteDepartmentCommand);
        }
        private void ExecuteGetAllSelectedDepartmentCommnan(object b)
        {     
            if(b != null)
            {
                ListView listView = b as ListView;
                List<Department> selectedItems = new List<Department>();
                foreach (object selectedItem in listView.SelectedItems)
                {
                    selectedItems.Add((Department)selectedItem);
                }
                DepartmentsIsSelected = new ObservableCollection<Department>(selectedItems);
            }
        }    
            

        private void ExecuteAddDepartmentCommand(object b)
        {
            if(DepartmentsIsSelected!=null)
            {
                foreach (Department department in DepartmentsIsSelected)
                {
                    projectAssignmentDao.Add(new ProjectAssignment(ID, department.ID));
                }
                LoadDepartmentsInProject(ID);
                LoadDepartmentsCanAssign(CreateProjectInstance());
            }                
            DepartmentsIsSelected = new ObservableCollection<Department>();
        }

        private void ExecuteDeleteDepartmentCommand(string departmentID)
        {
            projectAssignmentDao.Delete(ID, departmentID);
            LoadDepartmentsInProject(ID);
            LoadDepartmentsCanAssign(CreateProjectInstance());           
        }

        private void SearchByName()
        {
            var searchedItems = departmentsCanAssign;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = departmentsCanAssign
                    .Where(item => item.Name.Contains(textToSearch, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            SearchedDepartmentsCanAssign = new ObservableCollection<Department>(searchedItems);
        }

        public Project CreateProjectInstance()
        {
            return new Project(ID, Name, Utils.DateTimeToString(Start), Utils.DateTimeToString(End), Progress, projectStatus);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorMessage = "Tên không được để trống!!!";
                return false;
            }
            if (End < start)
            {
                ErrorMessage = "Thời gian kết thúc phải lớn hơn ngày bắt đầu!!!";
                return false;
            }
            return true;
        }

        public void TrimAllTexts()
        {
            id = id.Trim();
            name = name.Trim();
            progress = progress.Trim();
        }

        public void RetrieveProject(Project project)
        {
            ID = project.ID;
            Name = project.Name;
            Progress = project.Progress;
            Start = Utils.StringToDate(project.Start);
            End = Utils.StringToDate(project.End);
        }

    }
}
