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
using System.Windows.Documents;
using System.Security.Cryptography;
using System.Windows.Media;

namespace CompanyManagement.ViewModels.UserControls
{
    public class WidgetsViewModel : BaseViewModel, IRetrieveProjectID
    {
        #region Properties
        private SeriesCollection seriesTaskStatusViews; 
        public SeriesCollection SeriesTaskStatusViews { get => seriesTaskStatusViews; set { seriesTaskStatusViews = value; OnPropertyChanged(); } }

        private SeriesCollection seriesTaskProgressViews;
        public SeriesCollection SeriesTaskProgressViews { get => seriesTaskProgressViews; set { seriesTaskProgressViews = value; OnPropertyChanged(); } }

        private List<TeamStatusItem> listStatusTeam;
        public List<TeamStatusItem> ListStatusTeam { get => listStatusTeam; set { listStatusTeam = value; OnPropertyChanged(); } }

        private List<object> listOverdueWorkItem;
        public List<object> ListOverdueWorkItem { get => listOverdueWorkItem; set { listOverdueWorkItem = value; OnPropertyChanged(); } }

        public List<string> LabelsTaskProgressPercent { get; set; }
        public Func<int,string> LabelFormate { get; set; }

        private List<TaskInProject> TasksInProject;
        private List<Department> TeamsInProject;
        private List<Employee> EmployeesInProject;

        private string projectID = "";

        private TasksDao tasksDao = new TasksDao();
        private ProjectAssignmentsDao projectAssignmentsDao = new ProjectAssignmentsDao();
        #endregion

        public WidgetsViewModel() { }
       
        public void LoadLiveChartViews()
        {
            TasksInProject = tasksDao.SearchByProjectID(projectID);
            TeamsInProject = projectAssignmentsDao.GetAllDepartmentInProject(projectID);
            EmployeesInProject = projectAssignmentsDao.GetEmployeesInProject(projectID);
            SetSeriesTaskStatus();
            SetSeriesTaskProgress();
            LoadStatusTeam();
            LoadOverdueWorkItem();
        }

        #region Set Series 
        private void SetSeriesTaskProgress()
        {
            SeriesTaskProgressViews = new SeriesCollection();
            Dictionary<String, int> numberTasksInPercent = new Dictionary<String, int>() 
            { 
                { "0%", 0 }, { "10%", 0 }, { "20%", 0 }, 
                { "30%", 0 }, { "40%", 0 }, { "50%", 0 }, 
                { "60%", 0 }, { "70%", 0 }, { "80%", 0 }, 
                { "90%", 0 }, { "100%", 0 } 
            };

            foreach (var task in TasksInProject)
                numberTasksInPercent[task.Progress + "%"]++;

            SeriesTaskProgressViews.Add
                   (
                       new ColumnSeries()
                       {
                           Values = new ChartValues<int>(numberTasksInPercent.Values),
                           Fill = Brushes.Teal,
                           DataLabels = true
                       }                       
                   );

            SeriesTaskProgressViews.Add
                   (
                       new LineSeries()
                       {
                           Values = new ChartValues<int>(numberTasksInPercent.Values),
                           Stroke = Brushes.Red,
                           StrokeThickness = 0.5,
                           Fill = Brushes.Transparent, 
                       }
                   );   
            
            LabelsTaskProgressPercent = new List<string>(numberTasksInPercent.Keys);
            LabelFormate = value => ((int)value).ToString();
        }     

        private void SetSeriesTaskStatus()
        {
            Dictionary<string,int> numberTaskInStatus= new Dictionary<string, int>() 
            {
                { BaseDao.completedTask , 0 } , 
                { BaseDao.overdueTask , 0 } ,
                { BaseDao.ongoingTask , 0 } ,
                { BaseDao.underConsiderableTask , 0 }
            };

            foreach (var task in TasksInProject)
                numberTaskInStatus[task.StatusID]++;

            SeriesTaskStatusViews = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title ="Đã hoàn thành",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[BaseDao.completedTask],TasksInProject.Count)},
                    DataLabels= true       
                },
                new PieSeries
                {
                    Title ="Quá hạn",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[BaseDao.overdueTask],TasksInProject.Count)},
                    DataLabels= true
                },
                new PieSeries
                {
                    Title ="Đang thực hiện",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[BaseDao.ongoingTask],TasksInProject.Count)},
                    DataLabels= true
                },
                new PieSeries
                {
                    Title ="Đang xem xét",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[BaseDao.underConsiderableTask],TasksInProject.Count)},
                    DataLabels= true
                }
            };
        }
        #endregion

        #region Load
        private void LoadOverdueWorkItem()
        {
            var items = from task in TasksInProject
                        where task.StatusID == BaseDao.overdueTask
                        select new { Tile = task.Title, OverdueTime = (DateTime.Now.Date.Subtract(task.Deadline.Date)).Days };
            ListOverdueWorkItem = new List<object>(items.ToList());
        }

        private void LoadStatusTeam()
        {
            ListStatusTeam = new List<TeamStatusItem>();
            foreach (Department team in TeamsInProject)
                ListStatusTeam.Add(new TeamStatusItem(team.ID, team.Name, 0, 0, 0, 0, 0));

            var listNumberOverdueTasks = GetListNumberTaskByStatusOfTeam(BaseDao.overdueTask);

            var listNumberCompletedTasks = GetListNumberTaskByStatusOfTeam(BaseDao.completedTask);

            var listNumberOngoingTasks = GetListNumberTaskByStatusOfTeam(BaseDao.ongoingTask);

            var listNumberUnderConsiderableTasks = GetListNumberTaskByStatusOfTeam(BaseDao.underConsiderableTask);

            var listNumberAllTaskOpen = GetListNumberAllTaskOfTeam();

            foreach (TeamStatusItem team in ListStatusTeam)
            {
                var numberOverdueTask = listNumberOverdueTasks.FirstOrDefault(overdue => overdue.TeamID == team.TeamID);
                if (numberOverdueTask != null)
                {
                    team.NumberOverdueTasks = numberOverdueTask.Number;
                }

                var numberCompletedTask = listNumberCompletedTasks.FirstOrDefault(completed => completed.TeamID == team.TeamID);
                if (numberCompletedTask != null)
                {
                    team.NumberCompletedTasks = numberCompletedTask.Number;
                }

                var numberOngoingTask = listNumberOngoingTasks.FirstOrDefault(ongoing => ongoing.TeamID == team.TeamID);
                if (numberOngoingTask != null)
                {
                    team.NumberOngoingTasks = numberOngoingTask.Number;
                }

                var numberUnderConsiderableTask = listNumberUnderConsiderableTasks.FirstOrDefault(consider => consider.TeamID == team.TeamID);
                if (numberUnderConsiderableTask != null)
                {
                    team.NumberUnderConsiderableTasks = numberUnderConsiderableTask.Number;
                }

                var numberAllTaskOpen = listNumberAllTaskOpen.FirstOrDefault(open => open.TeamID == team.TeamID);
                if (numberAllTaskOpen != null)
                {
                    team.NumberAllTaskOpenTasks = numberAllTaskOpen.Number;
                }
                Log.Instance.Information(nameof(WidgetsViewModel), nameof(ListStatusTeam) + " = " + ListStatusTeam.Count);
            }
        }
        #endregion

        #region Get List Number Task Of Team
        private List<NumberTaskStatusOfTeam> GetListNumberTaskByStatusOfTeam(string StatusID)
        {
            var listItem = (from employee in EmployeesInProject
                            join team in TeamsInProject on employee.DepartmentID equals team.ID
                            join task in TasksInProject on employee.ID equals task.EmployeeID
                            where task.StatusID == StatusID
                            select new { TeamID = team.ID, TaskID = task.ID }).GroupBy(g => g.TeamID).Select(p => new NumberTaskStatusOfTeam() { TeamID = p.Key, Number = p.Count() });
            return new List<NumberTaskStatusOfTeam>(listItem);
        }

        private List<NumberTaskStatusOfTeam> GetListNumberAllTaskOfTeam()
        {
          var listItem = (from employee in EmployeesInProject
                            join team in TeamsInProject on employee.DepartmentID equals team.ID
                            join task in TasksInProject on employee.ID equals task.EmployeeID
                            select new { TeamID = team.ID, TaskID = task.ID }).GroupBy(g => g.TeamID).Select(p => new NumberTaskStatusOfTeam (){ TeamID = p.Key, Number = p.Count() });
            return new List<NumberTaskStatusOfTeam>(listItem);
        }
        #endregion

        #region Create Object 
        private class NumberTaskStatusOfTeam
        {
            public string TeamID { get; set; }
            public int Number { get; set; }

            public NumberTaskStatusOfTeam() { }
            public NumberTaskStatusOfTeam(string id, int number) 
            {
                TeamID = id;
                Number = number;
            }
        }
        
        public class TeamStatusItem
        {
            public string TeamID { get; set; }
            public string TeamName { get; set; }
            public int NumberOverdueTasks { get; set; }
            public int NumberAllTaskOpenTasks { get; set; }
            public int NumberCompletedTasks { get; set; }
            public int NumberUnderConsiderableTasks { get; set; }
            public int NumberOngoingTasks { get; set; }

            public TeamStatusItem (string id,string name, int numberOverdueTasks, int numberAllTaskOpenTasks, int numberCompletedTasks, int numberUnderConsiderableTasks, int numberOngoingTasks)
            {
                TeamID = id;
                TeamName = name;
                NumberOverdueTasks = numberOverdueTasks;
                NumberAllTaskOpenTasks = numberAllTaskOpenTasks;
                NumberCompletedTasks = numberCompletedTasks;
                NumberUnderConsiderableTasks = numberUnderConsiderableTasks;
                NumberOngoingTasks = numberOngoingTasks;
            }
            public TeamStatusItem() { }
        }
        #endregion


        private double GetPercent(int quantity, int totalQuantity)
        {
            return ((double)quantity / totalQuantity) * 100;
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            LoadLiveChartViews();
        }
    }
}
