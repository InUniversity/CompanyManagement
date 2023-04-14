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

namespace CompanyManagement.ViewModels.UserControls
{
    public class CheckOutViewModel : BaseViewModel, IDialogViewModel
    {
        private CheckInOut checkOut = new CheckInOut();

        private ObservableCollection<TaskInProject> tasksCompleted;
        public ObservableCollection<TaskInProject> TasksCompleted { get => tasksCompleted; set { tasksCompleted = value; } }
        
        public ICommand CheckOutCommand { get; set; }

        public IEditDBViewModel ParentDataContext { get; set; }

        private CompletedTaskDao completedTaskDao = new CompletedTaskDao();

        public CheckOutViewModel()
        {
            LoadTasksCompleted();
            CheckOutCommand = new RelayCommand<Window>(CheckOut);
        }

        private void LoadTasksCompleted()
        {
            var tasks = completedTaskDao.GetCompletedTasksWithoutTimeTracking(checkOut.EmployeeID, 
                Utils.ToFormatSQLServer(checkOut.CheckInTime));
            TasksCompleted = new ObservableCollection<TaskInProject>(tasks);
        }

        private void CheckOut(Window window)
        {
            AlertDialogService dialog = new AlertDialogService(
                "Check out",
                "Bạn có chắc chắn muốn check out không ?",
                () =>
                {
                    AddTaskCompletedToDB();
                    window.Close();
                }, () => {});
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
