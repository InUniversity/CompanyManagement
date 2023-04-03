using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class ManagerViewModel : BaseViewModel
    {
        
        private ContentControl currentChildView = new AssignmentUC();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private AssignmentUC assignmentUC = new AssignmentUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private WorkScheduleUC workScheduleUC = new WorkScheduleUC();
        private NotifyUC notifyUC = new NotifyUC();
        private SettingsUC settingsUC = new SettingsUC();

        public ICommand ShowAssignmentViewCommand { get; }
        public ICommand ShowEmployeesViewCommand { get; }
        public ICommand ShowWorkScheduleViewCommand { get; }
        public ICommand ShowNotifyViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public ManagerViewModel()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);         
        }

        private void ExecuteShowAssignmentView(object obj)
        {
            CurrentChildView.Content = assignmentUC;
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            CurrentChildView.Content = employeesUC;
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            CurrentChildView.Content = workScheduleUC;
        }

        private void ExecuteShowNotifyView(object obj)
        {
            CurrentChildView.Content = notifyUC;
        }
        
        private void ExecuteShowSettingsView(object obj)
        {
            CurrentChildView.Content = settingsUC;
        }
    }
}