using System;
using System.Collections.Generic;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;
using System.Windows;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TasksInProjectViewModel : BaseViewModel, IEditDBViewModel, IRetrieveProjectID
    {
        private List<TaskInProject> tasksInProject;
        public List<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private Visibility visibleAddButton = Visibility.Collapsed;
        public Visibility VisibleAddButton { get => visibleAddButton; set { visibleAddButton = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        private Visibility visibleUpdateButton = Visibility.Visible;
        public Visibility VisibleUpdateButton { get => visibleUpdateButton; set { visibleUpdateButton = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        private TaskInProjectDao taskInProjectDao;

        private string projectID = "";

        private string currentEmployeeID = CurrentUser.Instance.CurrentEmployee.ID;

        public TasksInProjectViewModel()
        {
            taskInProjectDao = new TaskInProjectDao();
            LoadTaskInProjects();
            SetVisible();
            SetCommands();
        }

        private void LoadTaskInProjects()
        {
            List<TaskInProject> tasks = CurrentUser.Instance.IsEmployee()
                ? taskInProjectDao.SearchByEmployeeID(projectID, currentEmployeeID) 
                : taskInProjectDao.SearchByProjectID(projectID);
            TasksInProject = tasks;
        }

        private void SetVisible()
        {
            if (!CurrentUser.Instance.IsEmployee())
            {
                VisibilityCRUD();
                VisibilityCRUDCommands();
            }
        }

        private void VisibilityCRUD()
        {
            visibleAddButton = Visibility.Visible;
            visibleDeleteButton = Visibility.Visible;
        }

        private void VisibilityCRUDCommands()
        {
            OpenTaskInProjectInputCommand = new RelayCommand<TaskInProject>(OpenAddDialog);
            DeleteTaskInProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
        }

        private void SetCommands()
        {
            UpdateTaskInProjectCommand = new RelayCommand<TaskInProject>(OpenUpdateDialog);
        }

        public void AddToDB(object task)
        {
            taskInProjectDao.Add(task as TaskInProject);
            LoadTaskInProjects();
        }

        public void UpdateToDB(object task)
        {
            taskInProjectDao.Update(task as TaskInProject);
            LoadTaskInProjects();
        }

        public void RetrieveProjectID(string projectID)
        {
            this.projectID = projectID;
            List<TaskInProject> tasks = CurrentUser.Instance.IsEmployee()
                ? taskInProjectDao.SearchByEmployeeID(projectID, currentEmployeeID)
                : taskInProjectDao.SearchByProjectID(projectID);
            TasksInProject = tasks;
        }

        private void OpenAddDialog(TaskInProject task)   
        {
            AddTaskDialog addTaskDialog = new AddTaskDialog();
            IDialogViewModel addTaskViewModel = (IDialogViewModel)addTaskDialog.DataContext;
            addTaskViewModel.ParentDataContext = this;
            task = CreateTaskInProjectInstance();
            addTaskViewModel.Retrieve(task);
            addTaskDialog.ShowDialog();
        }

        private TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), "", "", DateTime.Now , DateTime.Now , "",
                CurrentUser.Instance.CurrentAccount.EmployeeID, "", projectID, "1");
        }

        private void ExecuteDeleteCommand(string id)
        {
            taskInProjectDao.Delete(id);
            LoadTaskInProjects();
        }

        private void OpenUpdateDialog(TaskInProject task)
        {
            UpdateTaskDialog updateTaskDialog = new UpdateTaskDialog();
            IDialogViewModel updateTaskViewModel = (IDialogViewModel)updateTaskDialog.DataContext;
            updateTaskViewModel.ParentDataContext = this;
            updateTaskViewModel.Retrieve(task);
            updateTaskDialog.ShowDialog();
        }

        private string AutoGenerateID()
        {
            string taskInProjectID;
            Random random = new Random();
            do
            {
                int number = random.Next(1000000);
                taskInProjectID = $"T{number:000000}";
            } while (taskInProjectDao.SearchByID(taskInProjectID) != null);
            return taskInProjectID;
        }
    }
}
