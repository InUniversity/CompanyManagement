using CompanyManagement.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManagement.ViewModels
{
    public class ProjectViewModel:EmployeeViewModel
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

        private bool selected;
        public bool Selected { get => selected; set { selected = value; OnPropertyChanged(); } }

        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects { get => projects; set { projects = value; OnPropertyChanged(); }}

        public ICommand AddProjectCommand { get; set; }
        public ICommand DeleteProjectCommand { get; set; }
        public ICommand UpdateProjectCommand { get; set; }

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
            AddProjectCommand = new ReplayCommand<object>((p) => Add(p), (p) => CheckAllFields(p));
            DeleteProjectCommand = new ReplayCommand<object>((p) => Delete(p), (p) => id != null && projectDao.SearchByID(id) == null);
            UpdateProjectCommand = new ReplayCommand<object>((p) => Update(p));
        }

        private void Add(object p)
        { 
            Project project = new Project(this.id,this.name, DateTime.Now.ToString("dd-MM-yyyy"), this.end.ToString("dd-MM-yyyy"), "0%");
            projectDao.Add(project);
            LoadGVProjects();
        }

        private void Delete(object p)
        {
            Project project = new Project(this.id, this.name, this.start.ToString("dd-MM-yyyy"), this.end.ToString("dd-MM-yyyy"), this.progress);
            projectDao.Delete(project);
            LoadGVProjects();
        }

        private void Update(object p)
        {
            Project project = new Project(this.id, this.name, this.start.ToString("dd-MM-yyyy"), this.end.ToString("dd-MM-yyyy"), this.progress);
            projectDao.Save(project);
            LoadGVProjects();
        }
        private bool CheckAllFields(object p)
        {
            return true;
        }
    }
}
