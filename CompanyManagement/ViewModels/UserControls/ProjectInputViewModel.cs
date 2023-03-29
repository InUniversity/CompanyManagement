using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database.Implementations;
using CompanyManagement.Database.Interfaces;
using CompanyManagement.Models;
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

        private string progress = "0";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsInProject;
        public ObservableCollection<Department> DepartmentsInProject { get => departmentsInProject; set { departmentsInProject = value; OnPropertyChanged(); } }

        private bool isOpenAddDeparmentDialog = false;
        public bool IsOpenAddDeparmentDialog { get => isOpenAddDeparmentDialog; set { isOpenAddDeparmentDialog = value; OnPropertyChanged(); } }

        private List<Department> departmentsCanAssign;
        public List<Department> DepartmentsCanAssign { get => departmentsCanAssign; set { departmentsCanAssign = value; OnPropertyChanged(); } }

        public ICommand OpenAddDepartmentsDialog { get; set; }
        public ICommand CloseAddDepartmentDialog { get; set; }
        public ICommand AddDepartmentCommand { get; set; }
        public ICommand DeleteDeparmentCommand { get; set; }

        private IProjectAssignmentDao projectAssignmentDao;

        public ProjectInputViewModel(IProjectAssignmentDao projectAssignmentDao)
        {
            this.projectAssignmentDao = projectAssignmentDao;
            LoadDepartmentsInProject();
            LoadDepartmentsCanAssign();
            SetCommands();
        }

        private void LoadDepartmentsInProject()
        {
            var departments = projectAssignmentDao.GetAllDepartmentInProject(ID);
            // DepartmentsInProject = new ObservableCollection<Department>(new DepartmentDao().GetAll());
            var depart = new List<Department>()
            {
                new Department("hihi", "IT 1", "EM001"),
                new Department("hihi", "IT 2", "EM001"),
                new Department("hihi", "IT 3", "EM001"),
                new Department("hihi", "IT 4", "EM001"),
                new Department("hihi", "IT 5", "EM001"),
                new Department("hihi", "IT 6", "EM001")
            };
            DepartmentsInProject = new ObservableCollection<Department>(depart);
        }

        private void LoadDepartmentsCanAssign()
        {
            var departments = projectAssignmentDao.GetDepartmentsCanAssignWork("03/12/2000 12:00 AM", "03/12/2022 12:00 AM");
            //DepartmentsInProject = new ObservableCollection<Department>(departments);
            DepartmentsCanAssign = new DepartmentDao().GetAll();
        }

        private void SetCommands()
        {
            OpenAddDepartmentsDialog = new RelayCommand<object>(p => IsOpenAddDeparmentDialog = true);
            CloseAddDepartmentDialog = new RelayCommand<object>(p => IsOpenAddDeparmentDialog = false);
            AddDepartmentCommand = new RelayCommand<Department>(ExecuteAddDepartmentCommand);
            DeleteDeparmentCommand = new RelayCommand<string>(ExecuteDeleteDepartmentCommand);
        }

        private void ExecuteAddDepartmentCommand(Department department)
        {
            projectAssignmentDao.Add(new ProjectAssignment(ID, department.ID));
            LoadDepartmentsInProject();
            LoadDepartmentsCanAssign();
        }

        private void ExecuteDeleteDepartmentCommand(string departmentID)
        {
            MessageBox.Show("hihi");
            //projectAssignmentDao.Delete(new ProjectAssignment(ID, departmentID));
            //LoadDepartmentsInProject();
            //LoadDepartmentsCanAssign();
        }

        public Project CreateProjectInstance()
        {
            return new Project(ID, Name, Utils.DateTimeToString(Start), Utils.DateTimeToString(End), Progress);
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
