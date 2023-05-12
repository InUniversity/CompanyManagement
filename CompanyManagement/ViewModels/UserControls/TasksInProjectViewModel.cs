using System;
using System.Collections.Generic;
using CompanyManagement.Views.Dialogs;
using CompanyManagement.Models;
using System.Windows.Input;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.Base;
using System.Windows;
using CompanyManagement.Database.Base;
using CompanyManagement.Services;
using System.Linq;
using CompanyManagement.Enums;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TasksInProjectViewModel : BaseViewModel, IRetrieveProjectID
    {
        private List<TaskInProject> tasksInProject;
        public List<TaskInProject> TasksInProject 
        { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> ongoingTasksInProject;
        public List<TaskInProject> OngoingTasksInProject 
        { get => ongoingTasksInProject; set { ongoingTasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> completedTasksInProject;
        public List<TaskInProject> CompletedTasksInProject 
        { get => completedTasksInProject; set { completedTasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> overdueTasksInProject;
        public List<TaskInProject> OverdueTasksInProject 
        { get => overdueTasksInProject; set { overdueTasksInProject = value; OnPropertyChanged(); } }
        
        private List<TaskInProject> reviewingTasksInProject;
        public List<TaskInProject> ReviewingTasksInProject 
        { get => reviewingTasksInProject; set { reviewingTasksInProject = value; OnPropertyChanged(); } }
        
        private List<TaskInProject> cancelledTasksInProject;
        public List<TaskInProject> CancelledTasksInProject 
        { get => cancelledTasksInProject; set { cancelledTasksInProject = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton 
        { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; private set; }
        public ICommand DeleteTaskInProjectCommand { get; private set; }
        public ICommand UpdateTaskInProjectCommand { get; private set; }

        private TasksDao tasksDao = new TasksDao();
        private EmployeesDao employeesDao = new EmployeesDao();

        private string projectID = "";
        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public TasksInProjectViewModel()
        {
            LoadAllTasksSection();
            SetVisible();
            SetCommands();
        }

        private void LoadAllTasksSection()
        {
            TasksInProject = GetAllTasks();
            OngoingTasksInProject = TasksInProject.Where(p => p.Status == ETaskStatus.InProcess).ToList();
            CompletedTasksInProject = TasksInProject.Where(p => p.Status == ETaskStatus.Completed).ToList();
            OverdueTasksInProject = TasksInProject
                .Where(p => p.Progress != BaseDao.completed && p.Deadline > DateTime.Now).ToList();
            ReviewingTasksInProject = TasksInProject.Where(p => p.Status == ETaskStatus.Reviewing).ToList();
            CancelledTasksInProject = TasksInProject.Where(p => p.Status == ETaskStatus.Cancelled).ToList();
        }

        private List<TaskInProject> GetAllTasks()
        {
            var allTasks = currentEmployee.EmplRole.Perms == EPermission.NorEmpl
               ? tasksDao.SearchByEmployeeID(projectID, currentEmployee.ID)
               : tasksDao.SearchByProjectID(projectID);
            foreach (var task in allTasks)
            {
                task.AssignedEmployee = employeesDao.SearchByID(task.EmployeeID);
                task.Owner = employeesDao.SearchByID(task.OwnerID);
            }
            return allTasks;
        }

        private void SetVisible()
        {
            visibleDeleteButton = currentEmployee.EmplRole.Perms == EPermission.NorEmpl 
                ? Visibility.Collapsed : Visibility.Visible;
        }

        private void SetCommands()
        {
            OpenTaskInProjectInputCommand = new RelayCommand<object>(OpenAddDialog);
            DeleteTaskInProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
            UpdateTaskInProjectCommand = new RelayCommand<TaskInProject>(OpenUpdateDialog);
        }

        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            var tasks = currentEmployee.EmplRole.Perms == EPermission.NorEmpl
                ? tasksDao.SearchByEmployeeID(projectID, currentEmployee.ID)
                : tasksDao.SearchByProjectID(projectID);
            TasksInProject = tasks;
            LoadAllTasksSection();
        }

        private void OpenAddDialog(object obj)   
        {
            var task = CreateTaskInProjectInstance();
            var inputService = new InputDialogService<TaskInProject>(new AddTaskDialog(), task, Add);
            inputService.Show();
        }

        private void Add(object obj)
        {
            tasksDao.Add(obj as TaskInProject);
            LoadAllTasksSection();
        }

        private TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), "", "", DateTime.Now , DateTime.Now, 
                "0", currentEmployee.ID, "", projectID, ETaskStatus.InProcess, currentEmployee);
        }

        private void ExecuteDeleteCommand(string id)
        {
            var dialog = new AlertDialogService( 
                "Xóa nhiệm vụ", 
                "Bạn chắc chắn muốn xóa nhiệm vụ !",
                () =>
                {
                    tasksDao.Delete(id); 
                    LoadAllTasksSection();       
                }, null);
            dialog.Show();
        }

        private void OpenUpdateDialog(TaskInProject task)
        {
            task.Owner = employeesDao.SearchByID(task.OwnerID);
            task.AssignedEmployee = employeesDao.SearchByID(task.EmployeeID);
            var inputService = new InputDialogService<TaskInProject>(new UpdateTaskDialog(), task, Update);
            inputService.Show();
        }

        private void Update(TaskInProject task)
        {
            tasksDao.Update(task);
            LoadAllTasksSection();
        }

        private string AutoGenerateID()
        {
            string taskInProjectID;
            Random random = new Random();
            do
            {
                int number = random.Next(1000000);
                taskInProjectID = $"T{number:000000}";
            } while (tasksDao.SearchByID(taskInProjectID) != null);
            return taskInProjectID;
        }
    }
}
