using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class ManagerViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new AssignUC();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private TimeKeepingUC timeKeepingUC = new TimeKeepingUC();
        private TasksInProjectUC tasksInProjectUC = new TasksInProjectUC();
        private SettingsUC settingsUC = new SettingsUC();
        private NotifyUC notifyUC = new NotifyUC();
        private WorkScheduleUC workScheduleUC = new WorkScheduleUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private AssignUC assignUC = new AssignUC();

        public ICommand ShowAssignViewCommand { get; }
        public ICommand ShowEmployeesViewCommand { get; }
        public ICommand ShowWorkScheduleViewCommand { get; }
        public ICommand ShowNotifyViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public ManagerViewModel()
        {
            ShowAssignViewCommand = new RelayCommand<object>(ExecuteShowAssignView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);         
        }
        private void ExecuteShowSettingsView(object obj)
        {
            currentChildView.Content = settingsUC;
        }

        private void ExecuteShowNotifyView(object obj)
        {
            currentChildView.Content = notifyUC;
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            currentChildView.Content = workScheduleUC;
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            currentChildView.Content = employeesUC;
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = assignUC;
        }
    }
}
