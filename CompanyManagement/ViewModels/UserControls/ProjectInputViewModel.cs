using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private string progress = "";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsInProject;

        public ObservableCollection<Department> DepartmentsInProject { get => departmentsInProject; set { departmentsInProject = value; OnPropertyChanged(); } }

        public List<Department> Departments { get; set; }

        private IProjectAssignmentDao projectAssignmentDao;

        public ProjectInputViewModel(IProjectAssignmentDao projectAssignmentDao)
        {
            this.projectAssignmentDao = projectAssignmentDao;
            Departments = new List<Department>();
        }

        public void LoadDepartmentsInProject(string projectID)
        {
            var departments = projectAssignmentDao.GetAllDepartmentInProject(projectID);
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        public Project CreateProjectInstance()
        {
            return new Project(ID, Name, Utils.DateToString(Start), Utils.DateToString(End), Progress);
        }

        public bool CheckAllFields()
        {
            ErrorMessage = "";
            if (string.IsNullOrWhiteSpace(ID) || string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Progress))
            {
                ErrorMessage = "Các thông tin không được để trống!!!";
                return false;
            }
            if (End < start)
            {
                ErrorMessage = "Thời gian kết thúc không hợp lệ!!!";
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
