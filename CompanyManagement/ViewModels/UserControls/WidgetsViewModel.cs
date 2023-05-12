using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using CompanyManagement.Models;
using CompanyManagement.Utilities;
using System.Windows.Media;
using CompanyManagement.Enums;

namespace CompanyManagement.ViewModels.UserControls
{
    public class WidgetsViewModel : BaseViewModel, IRetrieveProjectID
    {
        private SeriesCollection seriesTaskStatusViews; 
        public SeriesCollection SeriesTaskStatusViews { get => seriesTaskStatusViews; set { seriesTaskStatusViews = value; OnPropertyChanged(); } }

        private SeriesCollection seriesTaskProgressViews;
        public SeriesCollection SeriesTaskProgressViews { get => seriesTaskProgressViews; set { seriesTaskProgressViews = value; OnPropertyChanged(); } }

        private List<TeamStatusItem> listStatusTeam;
        public List<TeamStatusItem> ListStatusTeam { get => listStatusTeam; set { listStatusTeam = value; OnPropertyChanged(); } }

        public List<string> LabelsTaskProgressPercent { get; set; }
        public Func<int, string> LabelFormat { get; set; }

        private List<TaskInProject> TasksInProject;
        private List<Department> TeamsInProject;
        private List<Employee> EmployeesInProject;

        private string projectID = "";

        private TasksDao tasksDao = new TasksDao();
        private ProjectAssignmentsDao projectAssignmentsDao = new ProjectAssignmentsDao();

        public WidgetsViewModel() { }
       
        public void LoadLiveChartViews()
        {
            TasksInProject = tasksDao.SearchByProjectID(projectID);
            TeamsInProject = projectAssignmentsDao.GetAllDepartmentInProject(projectID);
            EmployeesInProject = projectAssignmentsDao.GetEmployeesInProject(projectID);
            try
            {
                SetSeriesTaskStatus();
                SetSeriesTaskProgress();
                LoadStatusTeam();
            }
            catch (Exception ex)
            {
                Log.Instance.Error(nameof(WidgetsViewModel), ex.Message);
            }
        }

        private void SetSeriesTaskProgress()
        {
            SeriesTaskProgressViews = new SeriesCollection();
            Dictionary<string, int> numberTasksInPercent = new Dictionary<string, int>() 
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

            LabelsTaskProgressPercent = new List<string>(numberTasksInPercent.Keys);
            LabelFormat = value => value.ToString();
        }     

        private void SetSeriesTaskStatus()
        {
            var numberTaskInStatus= new Dictionary<int, int>() 
            {
                {(int)ETaskStatus.Completed, 0},
                {(int)ETaskStatus.Overdue, 0},
                {(int)ETaskStatus.InProcess, 0},
                {(int)ETaskStatus.Reviewing, 0},
                {(int)ETaskStatus.Cancelled, 0},
            };

            foreach (var task in TasksInProject)
                numberTaskInStatus[(int)task.Status]++;

            SeriesTaskStatusViews = new SeriesCollection()
            {
                new PieSeries()
                {
                    Title ="Đã hoàn thành",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[(int)ETaskStatus.Completed],TasksInProject.Count)},
                    DataLabels= true       
                },
                new PieSeries
                {
                    Title ="Quá hạn",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[(int)ETaskStatus.Overdue],TasksInProject.Count)},
                    DataLabels= true
                },
                new PieSeries
                {
                    Title ="Đang thực hiện",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[(int)ETaskStatus.InProcess],TasksInProject.Count)},
                    DataLabels= true
                },
                new PieSeries
                {
                    Title ="Đang xem xét",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[(int)ETaskStatus.Reviewing],TasksInProject.Count)},
                    DataLabels= true
                },
                new PieSeries
                {
                    Title ="Hủy",
                    Values = new ChartValues<double>(){GetPercent(numberTaskInStatus[(int)ETaskStatus.Cancelled],TasksInProject.Count)},
                    DataLabels= true
                }
            };
        }

        private void LoadStatusTeam()
        {
            ListStatusTeam = new List<TeamStatusItem>();
            foreach (Department team in TeamsInProject)
                ListStatusTeam.Add(new TeamStatusItem(team.ID, team.Name, 0, 0, 0, 0, 0));

            var listNumberOverdueTasks = GetListNumberTaskByStatusOfTeam(ETaskStatus.Overdue);

            var listNumberCompletedTasks = GetListNumberTaskByStatusOfTeam(ETaskStatus.Completed);

            var listNumberOngoingTasks = GetListNumberTaskByStatusOfTeam(ETaskStatus.InProcess);

            var listNumberUnderConsiderableTasks = GetListNumberTaskByStatusOfTeam(ETaskStatus.Reviewing);
            
            var listNumberCancelTasks = GetListNumberTaskByStatusOfTeam(ETaskStatus.Cancelled);

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
                
                var numberCancelTask = listNumberCancelTasks.FirstOrDefault(consider => consider.TeamID == team.TeamID);
                if (numberCancelTask != null)
                {
                    team.NumberCancelTasks = numberCancelTask.Number;
                }
            }
        }

        private List<NumberTaskStatusOfTeam> GetListNumberTaskByStatusOfTeam(ETaskStatus status)
        {
            var listItem = (from employee in EmployeesInProject
                            join team in TeamsInProject on employee.DepartmentID equals team.ID
                            join task in TasksInProject on employee.ID equals task.EmployeeID
                            where task.Status == status
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
            public int NumberCompletedTasks { get; set; }
            public int NumberUnderConsiderableTasks { get; set; }
            public int NumberOngoingTasks { get; set; }
            public int NumberCancelTasks { get; set; }

            public TeamStatusItem (string id,string name, int numberOverdueTasks, int numberCompletedTasks, int numberUnderConsiderableTasks, int numberOngoingTasks, int numberCancelTasks)
            {
                TeamID = id;
                TeamName = name;
                NumberOverdueTasks = numberOverdueTasks;
                NumberCompletedTasks = numberCompletedTasks;
                NumberUnderConsiderableTasks = numberUnderConsiderableTasks;
                NumberOngoingTasks = numberOngoingTasks;
                NumberCancelTasks = numberCancelTasks;
            }
        }


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
