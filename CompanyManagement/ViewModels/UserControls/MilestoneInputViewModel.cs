using CompanyManagement.Database;
using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public class MilestoneInputViewModel: BaseViewModel
    {
        private Milestone milestone;
        public Milestone MilestoneIns { get => milestone; set => milestone = value; }

        public string ID { get => milestone.ID; set { milestone.ID = value; OnPropertyChanged(); } }
        public string Title { get => milestone.Title; set { milestone.Title = value; OnPropertyChanged(); } }
        public string Explanation { get => milestone.Explanation; set { milestone.Explanation = value; OnPropertyChanged(); } }
        public DateTime Start { get => milestone.Start; set { milestone.Start = value; OnPropertyChanged(); LoadTasksCanAdd(); } }
        public DateTime End { get => milestone.End; set { milestone.End = value; OnPropertyChanged(); LoadTasksCanAdd(); } }
        public DateTime Completed { get => milestone.Completed; set { milestone.Completed = value; OnPropertyChanged(); } }
        public string OwnerID { get => milestone.OwnerID; set { milestone.OwnerID = value; OnPropertyChanged(); } }
        public int Progress { get => milestone.Progress; set { milestone.Progress = value; OnPropertyChanged(); } }
        public string ProjID { get => milestone.ProjID; set { milestone.ProjID = value; OnPropertyChanged(); } }
        public ObservableCollection<TaskInProject> TasksInMile
        { get => milestone.TasksInMile; set { milestone.TasksInMile = value; OnPropertyChanged(); } }

        private List<TaskInProject> tasksCanAdd = new List<TaskInProject>();

        private ObservableCollection<TaskInProject> searchedTasksCanAdd = new();
        public ObservableCollection<TaskInProject> SearchedTasksCanAdd
        { get => searchedTasksCanAdd; set { searchedTasksCanAdd = value; OnPropertyChanged(); } }

        private List<TaskInProject> selectedTasks = new List<TaskInProject>();
        private List<TaskInProject> SelectedTasks { get => selectedTasks; set { selectedTasks = value; OnPropertyChanged(); } }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByTitle();} }

        private string errorMessage = "";
        public string ErrorMessage { get => errorMessage; set { errorMessage = value; OnPropertyChanged(); } }

        private TasksDao tasksDao = new TasksDao();
        private MileTasksDao mileTasksDao = new MileTasksDao();

        public ICommand GetAllSelectedTasksCommand { get; private set; }
        public ICommand AddTaskCommand { get; private set; }
        public ICommand DeleteTaskCommand { get; private set; }

        public MilestoneInputViewModel()
        {
            milestone = new Milestone();
            SetCommands();
            LoadTasksCanAdd();
        }

        private void SetCommands()
        {
            GetAllSelectedTasksCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedTasks);
            AddTaskCommand = new RelayCommand<object>(ExecuteAddTask);
            DeleteTaskCommand = new RelayCommand<TaskInProject>(ExecuteDeleteTask);
        }

        private void LoadTasksCanAdd()
        {
            tasksCanAdd = mileTasksDao.SearchTasksCanAddToMile(milestone);
            SearchedTasksCanAdd = new ObservableCollection<TaskInProject>(tasksCanAdd);
        }

        private void ExecuteGetAllSelectedTasks(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<TaskInProject>().ToList();
            SelectedTasks = selectedItems;
        }

        private void ExecuteAddTask(object obj)
        {
            if (SelectedTasks != null)
            {
                foreach (var task in SelectedTasks)
                {
                    TasksInMile.Add(task);
                    tasksCanAdd.Remove(task);
                    SearchedTasksCanAdd.Remove(task);
                }
            }
            SelectedTasks = null;
        }

        private void ExecuteDeleteTask(TaskInProject task)
        {
            TasksInMile.Remove(task);
            tasksCanAdd.Add(task);
            SearchedTasksCanAdd.Add(task);
        }

        private void SearchByTitle()
        {
            var searchedItems = tasksCanAdd;
            if (!string.IsNullOrWhiteSpace(textToSearch))
            {
                searchedItems = tasksCanAdd
                    .Where(item => item.Title.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedTasksCanAdd = new ObservableCollection<TaskInProject>(searchedItems);
        }

        public void TrimAllTexts()
        {
            ID = ID.Trim();
            Title = Title.Trim();
            Explanation = Explanation.Trim();
        }
    }
}
