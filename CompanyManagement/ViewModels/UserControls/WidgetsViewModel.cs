using CompanyManagement.Database.Base;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System.Threading.Tasks;

namespace CompanyManagement.ViewModels.UserControls
{
    public class WidgetsViewModel : BaseViewModel, IRetrieveProjectID
    {
        private SeriesCollection seriesTaskStatusViews; 
        public SeriesCollection SeriesTaskStatusViews { get => seriesTaskStatusViews; set { seriesTaskStatusViews = value; OnPropertyChanged(); } }

        private SeriesCollection seriesTaskProgressViews;
        public SeriesCollection SeriesTaskProgressViews { get => seriesTaskProgressViews; set { seriesTaskProgressViews = value; OnPropertyChanged(); } }       

        private List<object> listStatusTeam;
        public List<object> ListStatusTeam { get => listStatusTeam; set { listStatusTeam = value; OnPropertyChanged(); } }

        private List<object> listOverdueWorkItem;
        public List<object> ListOverdueWorkItem { get => listOverdueWorkItem; set { listOverdueWorkItem = value; OnPropertyChanged(); } }

        public List<string> LabelsTaskProgressPercent { get; set; }
        public Func<int,string> FormateLabels { get; set; }


        private List<TaskInProject> TasksInProject;

        private string projectID = "";

        private TasksDao tasksDao = new TasksDao();
        private ProjectAssignmentsDao projectAssignmentsDao = new ProjectAssignmentsDao();

        public WidgetsViewModel() 
        {
            
            LoadLiveChartViews();          
        }

        private void SetSeriesTaskStatus()
        {
            double countCompleted = 0.0;
            double completedPercent =0.0;
            foreach (var task in TasksInProject)
                if (task.Progress == BaseDao.completed)
                    countCompleted++;
            Log.Instance.Information(nameof(WidgetsViewModel), "count completed = " + countCompleted.ToString());
            if (countCompleted > 0) 
                completedPercent = (countCompleted / TasksInProject.Count()) * 100;
            Log.Instance.Information(nameof(WidgetsViewModel), "completed = " + completedPercent.ToString());
            SeriesTaskStatusViews = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title ="Đã hoàn thành",
                    Values = new ChartValues<double>(){completedPercent},
                    DataLabels= true       
                },
                new PieSeries
                {
                    Title ="Chưa hoàn thành",
                    Values = new ChartValues<double>(){100.0 - completedPercent},
                    DataLabels= true
                }
            };
        }

        private void LoadStatusTeam()
        {
            List<Department> ListTeam = projectAssignmentsDao.GetAllDepartmentInProject(projectID);
            List<Employee> ListEmployee = projectAssignmentsDao.GetEmployeesInProject(projectID);
            var listOverdue = from employee in ListEmployee
                              join team in ListTeam on employee.DepartmentID equals team.ID
                              join task in TasksInProject on employee.ID equals task.EmployeeID
                              where task.Deadline < DateTime.Now && task.Progress != BaseDao.completed
                              select new { TeamID = team.ID, TaskID = task.ID };
            var countOverdue = listOverdue.GroupBy(g => g.TeamID).Select(p => new { TeamID = p.Key, Count = p.Count() });

            var listAllTaskOpen = from employee in ListEmployee
                                  join team in ListTeam on employee.DepartmentID equals team.ID
                                  join task in TasksInProject on employee.ID equals task.EmployeeID
                                  where task.Deadline >= DateTime.Now && task.Progress != BaseDao.completed
                                  select new { TeamID = team.ID, TaskID = task.ID };
            var countAllTaskOpen = listAllTaskOpen.GroupBy(g => g.TeamID).Select(p => new { TeamID = p.Key, Count = p.Count() });

            var listCompletedTask = from employee in ListEmployee
                                join team in ListTeam on employee.DepartmentID equals team.ID
                                join task in TasksInProject on employee.ID equals task.EmployeeID
                                where task.Progress == BaseDao.completed
                                select new { TeamID = team.ID, TaskID = task.ID };
            var countCompletedTask = listCompletedTask.GroupBy(g => g.TeamID).Select(p => new { TeamID = p.Key, Count = p.Count() });

            var listItem = from team in ListTeam
                           join overdue in countOverdue on team.ID equals overdue.TeamID
                           join allopen in countAllTaskOpen on team.ID equals allopen.TeamID
                           join completed in countCompletedTask on team.ID equals completed.TeamID
                           select new { Name = team.Name, Overdue = overdue.Count, Completed = completed.Count, AllOpen = allopen.Count };
            ListStatusTeam = new List<object>(listItem.ToList());
        }

        private void LoadOverdueWorkItem()
        {
            var items = from task in TasksInProject
                        where task.Deadline < DateTime.Now && task.Progress != BaseDao.completed
                        select new { Tile = task.Title, OverdueTime = (DateTime.Now.Date.Subtract(task.Deadline.Date)).Days };
            ListOverdueWorkItem = new List<object>(items.ToList());
        }

        public void LoadLiveChartViews()
        {
            TasksInProject = tasksDao.SearchByProjectID(projectID);
            SetSeriesTaskStatus();
            SetSeriesTaskProgress();
            LoadStatusTeam();
            LoadOverdueWorkItem();
        }

        private void SetSeriesTaskProgress()
        {           
            SeriesTaskProgressViews = new SeriesCollection();
            Dictionary<String, int> numberTasksInPercent = new Dictionary<String, int>() { { "0%" , 0 }, { "10%", 0 }, { "20%", 0 }, { "30%", 0 }, { "40%", 0 }, { "50%", 0 }, { "60%", 0 }, { "70%", 0 }, { "80%", 0 }, { "90%", 0 }, { "100%", 0 } };
            foreach (var task in TasksInProject)
                numberTasksInPercent[task.Progress+ "%"] ++;
            SeriesTaskProgressViews.Add
                   (
                       new ColumnSeries()
                       {
                           Title= "Task Count",
                           Values = new ChartValues<int>(numberTasksInPercent.Values) ,
                           DataLabels = true,
                       }
                   );
            LabelsTaskProgressPercent = new List<string>(numberTasksInPercent.Keys);
            FormateLabels = value => value.ToString();
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadLiveChartViews();
        }
    }
}
