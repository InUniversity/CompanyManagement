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
using CompanyManagement.Utilities;
using CompanyManagement.Strategies.UserControls.ProjectsView;
using CompanyManagement.Views.UserControls;
using System.Linq;

namespace CompanyManagement.ViewModels.UserControls
{
    public class TasksInProjectViewModel : BaseViewModel, IRetrieveProjectID
    {
        private List<TaskInProject> tasksInProject;
        public List<TaskInProject> TasksInProject { get => tasksInProject; set { tasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> ongoingTasksInProject;
        public List<TaskInProject> OngoingTasksInProject { get => ongoingTasksInProject; set { ongoingTasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> completedTasksInProject;
        public List<TaskInProject> CompletedTasksInProject { get => completedTasksInProject; set { completedTasksInProject = value; OnPropertyChanged(); } }

        private List<TaskInProject> overdueTasksInProject;
        public List<TaskInProject> OverdueTasksInProject { get => overdueTasksInProject; set { overdueTasksInProject = value; OnPropertyChanged(); } }

        private Visibility visibleAddButton = Visibility.Collapsed;
        public Visibility VisibleAddButton { get => visibleAddButton; set { visibleAddButton = value; OnPropertyChanged(); } }

        private Visibility visibleDeleteButton = Visibility.Collapsed;
        public Visibility VisibleDeleteButton { get => visibleDeleteButton; set { visibleDeleteButton = value; OnPropertyChanged(); } }

        private Visibility visibleUpdateButton = Visibility.Visible;
        public Visibility VisibleUpdateButton { get => visibleUpdateButton; set { visibleUpdateButton = value; OnPropertyChanged(); } }

        public ICommand OpenTaskInProjectInputCommand { get; set; }
        public ICommand DeleteTaskInProjectCommand { get; set; }
        public ICommand UpdateTaskInProjectCommand { get; set; }

        private TaskInProjectDao taskInProjectDao = new TaskInProjectDao();

        private string projectID = "";

        private Employee currentEmployee = CurrentUser.Ins.EmployeeIns;

        public TasksInProjectViewModel()
        {
            LoadTaskInProjects();
            SetVisible();
            SetCommands();
        }

        private void LoadTaskInProjects()
        {
            TasksInProject = string.Equals(currentEmployee.PositionID, BaseDao.EMPLOYEE_POS_ID)
               ? taskInProjectDao.SearchByEmployeeID(projectID, currentEmployee.ID)
               : taskInProjectDao.SearchByProjectID(projectID);

            var listOngoingTasks = TasksInProject.Where(p => p.Progress != BaseDao.COMPLETED && p.Deadline > DateTime.Now).ToList();
            if (listOngoingTasks.Count > 0)
            {
                OngoingTasksInProject = new List<TaskInProject>(listOngoingTasks);
            }

            var listCompletedTasks = TasksInProject.Where(p => p.Progress == BaseDao.COMPLETED).ToList();
            if (listCompletedTasks.Count > 0)
            {
                CompletedTasksInProject = new List<TaskInProject>(listCompletedTasks);
            }

            var listOverdueTasks = TasksInProject.Where(p => p.Deadline < DateTime.Now && p.Progress != BaseDao.COMPLETED).ToList();
            if (listOverdueTasks.Count > 0)
            {
                OverdueTasksInProject = new List<TaskInProject>(listOverdueTasks);
            }
        }

        private void SetVisible()
        {
            if (!string.Equals(currentEmployee.PositionID, BaseDao.EMPLOYEE_POS_ID))
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
            OpenTaskInProjectInputCommand = new RelayCommand<object>(OpenAddDialog);
            DeleteTaskInProjectCommand = new RelayCommand<string>(ExecuteDeleteCommand);
        }

        private void SetCommands()
        {
            UpdateTaskInProjectCommand = new RelayCommand<TaskInProject>(OpenUpdateDialog);
        }


        public void ReceiveProjectID(string projectID)
        {
            this.projectID = projectID;
            List<TaskInProject> tasks = string.Equals(CurrentUser.Ins.EmployeeIns.PositionID, BaseDao.EMPLOYEE_POS_ID)
                ? taskInProjectDao.SearchByEmployeeID(projectID, currentEmployee.ID)
                : taskInProjectDao.SearchByProjectID(projectID);
            TasksInProject = tasks;
        }

        private void OpenAddDialog(object obj)   
        {
            TaskInProject task = CreateTaskInProjectInstance();
            var inputService = new InputDialogService<TaskInProject>(new AddTaskDialog(), task, Add);
            inputService.Show();
        }

        private void Add(object obj)
        {
            taskInProjectDao.Add(obj as TaskInProject);
            LoadTaskInProjects();
        }

        private TaskInProject CreateTaskInProjectInstance()
        {
            return new TaskInProject(AutoGenerateID(), "", "", DateTime.Now , DateTime.Now , 
                "", CurrentUser.Ins.EmployeeIns.ID, "", projectID, "1");
        }

        private void ExecuteDeleteCommand(string id)
        {
            AlertDialogService dialog = new AlertDialogService(
             "Xóa nhiệm vụ",
             "Bạn chắc chắn muốn xóa nhiệm vụ !",
             () =>
             {
                 taskInProjectDao.Delete(id); 
                 LoadTaskInProjects();       
             }, () => { });
            dialog.Show();
        }

        private void OpenUpdateDialog(TaskInProject task)
        {
            var inputService = new InputDialogService<TaskInProject>(new UpdateTaskDialog(), task, Update);
            inputService.Show();
        }

        private void Update(TaskInProject task)
        {
            taskInProjectDao.Update(task);
            LoadTaskInProjects();
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
