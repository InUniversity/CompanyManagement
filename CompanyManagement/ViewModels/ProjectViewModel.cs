using CompanyManagement.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ProjectViewModel:BaseViewModel
    {
        private string id;
        public string ID { get => id; set { id = value; OnPropertyChanged(); } }
        private string name;
        public string Name { get => name; set { name = value; OnPropertyChanged(); } }
        private DateTime start;
        public DateTime Start { get => start; set { start = value; OnPropertyChanged(); } }
        private DateTime end;
        public DateTime End { get => end; set { end = value; OnPropertyChanged(); } }
        private string progress;
        public string Progress { get => progress; set { progress = value; OnPropertyChanged(); } }

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); }}

        public ICommand AddProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand DeleteAllProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }
        public ICommand OpenWindowProjectDetailsDialogCommand { get; set; }

        private ProjectDao projectDao = new ProjectDao();
        
        public ProjectViewModel()
        {
            LoadGVProjects();
            SetCommands();
        }

        private void LoadGVProjects()
        {
            Projects = new ObservableCollection<Project>();
            DataTable dataTable = projectDao.GetDataTable();
            foreach (DataRow row in dataTable.Rows)
            {
                Project project = new Project(row);
                Projects.Add(project);
            }
        }

        private void SetCommands()
        {
            AddProjectCommand = new RelayCommand<object>((p) => Add(p), (p) => CheckAllFields(p));
            DeleteProjectCommand = new RelayCommand<object>(ExecuteDeleteCommand);
            UpdateProjectCommand = new RelayCommand<object>(ExecuteUpdateCommand);
            DeleteAllProjectCommand = new RelayCommand<object>(ExecuteDeleteAllCommand);
            OpenWindowProjectDetailsDialogCommand = new RelayCommand<object>(ExecuteOpenWindow);
        }

        private void Add(object p)
        { 
            Project project = new Project(this.id,this.name, DateTime.Now.ToString("dd-MM-yyyy"), this.end.ToString("dd-MM-yyyy"), "0%");
            projectDao.Add(project);
            LoadGVProjects();
        } 
        private void ExecuteDeleteCommand(object b)
        {
            //if (selectedProject != null)
            //{
            //    projectDao.Delete(this.id);
            //    LoadGVProjects();
            //}
            LoadGVProjects();
        }
        private void ExecuteDeleteAllCommand(object b)
        {           
            projectDao.DeleteAll();
            LoadGVProjects();          
        }
        private void ExecuteUpdateCommand(object p)
        {
            //if(selectedProject!= null)
            //{
            //    Project project = new Project(this.id, this.name, selectedProject.Start, selectedProject.End, this.progress);
            //    projectDao.Save(project);
            //    LoadGVProjects();
            //}
            LoadGVProjects();
        }
        private void ExecuteOpenWindow(object p)
        {
            ProjectDetailsDialog projectDetailsDialog = new ProjectDetailsDialog();
            projectDetailsDialog.ShowDialog();
        }
        private bool CheckAllFields(object p)
        {
            return true;
        }
    }
}
