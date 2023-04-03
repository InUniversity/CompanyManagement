using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class ManagerViewModel : BaseViewModel
    {
        
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private AssignmentUC assignmentUC = new AssignmentUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private WorkScheduleUC workScheduleUC = new WorkScheduleUC();
        private NotifyUC notifyUC = new NotifyUC();
        private SettingsUC settingsUC = new SettingsUC();

        public ICommand ShowAssignmentViewCommand { get; set; }
        public ICommand ShowEmployeesViewCommand { get; set; }
        public ICommand ShowWorkScheduleViewCommand { get; set; }
        public ICommand ShowNotifyViewCommand { get; set; }
        public ICommand ShowSettingsViewCommand { get; set; }

        public ManagerViewModel()
        {
            CurrentChildView = assignmentUC;
            SetCommands();
        }

        private void SetCommands()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);         
        }

        private void ExecuteShowAssignmentView(object obj)
        {
            CurrentChildView = assignmentUC;
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            CurrentChildView = employeesUC;
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            CurrentChildView = workScheduleUC;
        }

        private void ExecuteShowNotifyView(object obj)
        {
            CurrentChildView = notifyUC;
        }
        
        private void ExecuteShowSettingsView(object obj)
        {
            CurrentChildView = settingsUC;
        }
    }
}