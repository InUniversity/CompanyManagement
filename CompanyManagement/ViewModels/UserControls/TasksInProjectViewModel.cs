using System;
using System.Collections.Generic;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.ViewModels.Dialogs.Interfaces;
using CompanyManagement.ViewModels.UserControls.Interfaces;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TasksInProjectViewModel : BaseViewModel, IEditDBViewModel, IRetrieveProjectID
    {
        private List<TaskInProject> tasksInProject;
        public List<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        private TaskInProjectDao taskInProjectDao;
        
        private string projectID = "";

        public TasksInProjectViewModel()
        {
            taskInProjectDao = new TaskInProjectDao();
            LoadTaskInProjects();
            SetCommands();
        }

        private void LoadTaskInProjects()
        {
            TasksInProject = taskInProjectDao.SearchByProjectID(projectID);
        }

        private void SetCommands()
        {
            OpenTaskInProjectInputCommand = new RelayCommand<TaskInProject>(OpenAddDialog);
            DeleteTaskInProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
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
            TasksInProject = taskInProjectDao.SearchByProjectID(projectID);
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
