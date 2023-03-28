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

        private string progress = "0";
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private int projectStatus = 1;
        public int ProjectStatus { get => projectStatus; set { projectStatus = value; OnPropertyChanged(); } }  

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsInProject;
        public ObservableCollection<Department> DepartmentsInProject { get => departmentsInProject; set { departmentsInProject = value; OnPropertyChanged(); } }

        private ObservableCollection<Department> departmentsCanAssign;
        public ObservableCollection<Department> DepartmentsCanAssign { get => departmentsCanAssign; set { departmentsCanAssign = value; OnPropertyChanged(); } }

        public List<ProjectStatus> ProjectStatuses { get; set; }

        private IProjectAssignmentDao projectAssignmentDao;
        private IProjectStatusDao projectStatusDao;

        public ProjectInputViewModel(IProjectAssignmentDao projectAssignmentDao, IProjectStatusDao projectStatusDao)
        {
            this.projectStatusDao = projectStatusDao;
            this.projectAssignmentDao = projectAssignmentDao;
            LoadDepartmentsInProject();
            LoadDepartmentsCanAssign();
        }

        public void SetAllComboBox()
        {
            ProjectStatuses = projectStatusDao.GetAll();
        }

        private void LoadDepartmentsInProject()
        {
            var departments = projectAssignmentDao.GetAllDepartmentInProject(ID);
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        private void LoadDepartmentsCanAssign()
        {
            var departments = projectAssignmentDao.GetDepartmentsCanAssignWork("03/12/2000 12:00 AM", "03/12/2022 12:00 AM");
            DepartmentsInProject = new ObservableCollection<Department>(departments);
        }

        public Project CreateProjectInstance()
        {
            return new Project(id, name, Utils.DateToString(start), Utils.DateToString(end), progress, projectStatus);
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
