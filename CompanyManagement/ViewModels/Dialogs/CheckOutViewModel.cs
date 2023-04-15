using CompanyManagement.Models;
using CompanyManagement.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.Services;
using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;
using System.Threading.Tasks;

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckOutViewModel : BaseViewModel, IDialogViewModel
    {
        private CheckInOut checkOut = new CheckInOut();

        private ObservableCollection<TaskInProject> tasksCompleted;
        public ObservableCollection<TaskInProject> TasksCompleted { get => tasksCompleted; set { tasksCompleted = value; OnPropertyChanged(); } }

        private List<TaskInProject> TasksCanChoose;

        private ObservableCollection<TaskInProject> searchedTasksCanChoose;
        public ObservableCollection<TaskInProject> SearchedTasksCanChoose
        { get => searchedTasksCanChoose; set { searchedTasksCanChoose = value; OnPropertyChanged(); } }

        private List<TaskInProject> selectedTasks;
        public List<TaskInProject> SelectedTasks { get => selectedTasks; set => selectedTasks = value; }

        private string textToSearch = "";
        public string TextToSearch { get => textToSearch; set { textToSearch = value; OnPropertyChanged(); SearchByName(); } }


        public ICommand CheckOutCommand { get; set; }
        public ICommand AddTaskCompletedCommand { get; set; }
        public ICommand GetAllSelectedTasksCommand { get; set; }
        public ICommand DeleteTaskCompletedCommand { get; set; }
        public IEditDBViewModel ParentDataContext { get; set; }

        private CompletedTaskDao completedTaskDao = new CompletedTaskDao();

        public CheckOutViewModel()
        {
            LoadTasksCompleted();
            SetCommands();
            LoadTasksCanChoose();
            TasksCompleted = new ObservableCollection<TaskInProject>();
        }
        private void SetCommands()
        {
            CheckOutCommand = new RelayCommand<Window>(ExecuteCheckOutCommand);
            GetAllSelectedTasksCommand = new RelayCommand<ListView>(ExecuteGetAllSelectedTasksCommand);
            AddTaskCompletedCommand = new RelayCommand<object>(ExecuteAddTaskCompletedCommand);
            DeleteTaskCompletedCommand = new RelayCommand<TaskInProject>(ExecuteDeleteTaskCompletedCommand);
        }

        private void ExecuteCheckOutCommand(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
                 "Check out",
                 "Bạn có chắc chắn muốn check out không ?",
                 () =>
                 {
                     AddTaskCompletedToDB();
                     window.Close();
                 }, () => { });
            dialog.Show();
        }

        private void ExecuteDeleteTaskCompletedCommand(TaskInProject task)
        {
            TasksCompleted.Remove(task);
            LoadTasksCompleted();
            LoadTasksCanChoose();
        }

        private void ExecuteAddTaskCompletedCommand(object b)
        {
            if (SelectedTasks != null)
            {
                foreach (TaskInProject task in SelectedTasks)
                {
                    Log.Instance.Information("CheckoutViewModel","selected task: "+ task.Title);
                    TasksCompleted.Add(task);
                }
                LoadTasksCompleted();
                LoadTasksCanChoose();
            }
            SelectedTasks = new List<TaskInProject>();
        }

        private void ExecuteGetAllSelectedTasksCommand(ListView listView)
        {
            var selectedItems = listView.SelectedItems.Cast<TaskInProject>().ToList();
            SelectedTasks = selectedItems;
        }

        private void LoadTasksCompleted()
        {
            //var tasks = completedTaskDao.GetCompletedTasksWithoutTimeTracking(checkOut.EmployeeID, 
            //    Utils.ToFormatSQLServer(checkOut.CheckInTime));
            //TasksCompleted = new ObservableCollection<TaskInProject>(tasks);
        }
        private void LoadTasksCanChoose()
        {
            TasksCanChoose = new List<TaskInProject>(new TaskInProjectDao().SearchByProjectID("PRJ001"));
            SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(TasksCanChoose);
            
        }
        private void SearchByName()
        {
            var searchedItems = TasksCanChoose;
            if (!string.IsNullOrEmpty(textToSearch))
            {
                searchedItems = TasksCanChoose
                    .Where(item => item.Title.ToLower().Contains(textToSearch.ToLower()))
                    .ToList();
            }
            SearchedTasksCanChoose = new ObservableCollection<TaskInProject>(searchedItems);
        }

        private void AddTaskCompletedToDB()
        {
           foreach (var task in TasksCompleted)
           {
               var completedTask = new CompletedTask(checkOut.CompletedTaskID, task.ID);
                completedTaskDao.Add(completedTask);
           }
        }
        
        public void Retrieve(object obj)
        {
            checkOut = obj as CheckInOut;
        }
    }
}
