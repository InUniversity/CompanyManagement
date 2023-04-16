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
        private UserInformationUC userInformationUC = new UserInformationUC();
        private LeavesUC leavesUC = new LeavesUC();
        
        private bool statusSettingsView = false;
        public bool StatusSettingsView { get => statusSettingsView; set { statusSettingsView = value; OnPropertyChanged(); } }

        private bool statusNotifyView = false;
        public bool StatusNotifyView { get => statusNotifyView; set { statusNotifyView = value; OnPropertyChanged(); } }

        private bool statusWorkScheduleView = false;
        public bool StatusWorkScheduleView { get => statusWorkScheduleView; set { statusWorkScheduleView = value; OnPropertyChanged(); } }

        private bool statusEmployeesView = false;
        public bool StatusEmployeesView { get => statusEmployeesView; set { statusEmployeesView = value; OnPropertyChanged(); } }

        private bool statusAssignmentView = false;
        public bool StatusAssignmentView { get => statusAssignmentView; set { statusAssignmentView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }

        private bool statusLeavesView = false;
        public bool StatusLeavesView { get => statusLeavesView; set { statusLeavesView = value; OnPropertyChanged(); } }

        public ICommand ShowAssignmentViewCommand { get; set; }
        public ICommand ShowEmployeesViewCommand { get; set; }
        public ICommand ShowWorkScheduleViewCommand { get; set; }
        public ICommand ShowNotifyViewCommand { get; set; }
        public ICommand ShowSettingsViewCommand { get; set; }
        public ICommand ShowUserInformationViewCommand { get; set; }
        public ICommand ShowLeavesViewCommand { get; set;}

        public ManagerViewModel()
        {
            ExecuteShowUserInformationViewCommand(null);
            SetCommands();
        }

        private void SetCommands()
        {
            ShowAssignmentViewCommand = new RelayCommand<object>(ExecuteShowAssignmentView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
            ShowLeavesViewCommand = new RelayCommand<object>(ExecuteShowLeavesViewCommand);
        }

        private void ExecuteShowLeavesViewCommand(object obj)
        {
            CurrentChildView = leavesUC;
            StatusLeavesView = true;
        }

        private void ExecuteShowUserInformationViewCommand(object obj)
        {
            CurrentChildView = userInformationUC;
            StatusUserInformationView = true;
        }

        private void ExecuteShowAssignmentView(object obj)
        {
            CurrentChildView = assignmentUC;
            StatusAssignmentView = true;
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            CurrentChildView = employeesUC;
            StatusEmployeesView = true; 
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            CurrentChildView = workScheduleUC;
            StatusWorkScheduleView = true;
        }

        private void ExecuteShowNotifyView(object obj)
        {
            CurrentChildView = notifyUC;
            StatusNotifyView = true;
        }
        
        private void ExecuteShowSettingsView(object obj)
        {
            CurrentChildView = settingsUC;
            StatusSettingsView = true;
        }
    }
}