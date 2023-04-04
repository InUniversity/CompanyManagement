using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Linq;
using System.Windows.Controls;
using CompanyManagement.Database.Implementations;

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

        private string createBy = SingletonEmployee.Instance.CurrentAccount.EmployeeID;
        public string CreateBy { get => createBy; set { createBy = value; OnPropertyChanged(); } }

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
        public ICommand GetAllSelectedDepartmentCommand { get; set; }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        private IProjectStatusDao projectStatusDao;
        private IProjectAssignmentDao projectAssignmentDao;

        public ProjectInputViewModel()
        {
            this.projectStatusDao = new ProjectStatusDao();
            this.projectAssignmentDao = new ProjectAssignmentDao();
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
            DepartmentsIsSelected = new ObservableCollection<Department>(selectedItems);
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
            return new Project(id, name, start, end, completed, progress, projectStatusID, createBy);
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
