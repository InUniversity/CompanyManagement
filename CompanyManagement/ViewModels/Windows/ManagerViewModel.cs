using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.CodeDom.Compiler;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class ManagerViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new UserInformationUC();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }
       
        private SettingsUC settingsUC = new SettingsUC();
        private NotifyUC notifyUC = new NotifyUC();
        private WorkScheduleUC workScheduleUC = new WorkScheduleUC();
        private EmployeesUC employeesUC = new EmployeesUC();
        private AssignUC assignUC = new AssignUC();
        private UserInformationUC userInformationUC = new UserInformationUC();

        private bool statusSettingsView = false;
        public bool StatusSettingsView { get => statusSettingsView; set { statusSettingsView = value; OnPropertyChanged(); } }

        private bool statusNotifyView = false;
        public bool StatusNotifyView { get => statusNotifyView; set { statusNotifyView = value; OnPropertyChanged(); } }

        private bool statusWorkScheduleView = false;
        public bool StatusWorkScheduleView { get => statusWorkScheduleView; set { statusWorkScheduleView = value; OnPropertyChanged(); } }

        private bool statusEmployeesView = false;
        public bool StatusEmployeesView { get => statusEmployeesView; set { statusEmployeesView = value; OnPropertyChanged(); } }

        private bool statusAssignView = false;
        public bool StatusAssignView { get => statusAssignView; set { statusAssignView = value; OnPropertyChanged(); } }

        private bool statusUserInformationView = false;
        public bool StatusUserInformationView { get => statusUserInformationView; set { statusUserInformationView = value; OnPropertyChanged(); } }



        public ICommand ShowAssignViewCommand { get; }
        public ICommand ShowEmployeesViewCommand { get; }
        public ICommand ShowWorkScheduleViewCommand { get; }
        public ICommand ShowNotifyViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowUserInformationViewCommand { get; }

        public ManagerViewModel()
        {
            StatusUserInformationView= true;
            ShowAssignViewCommand = new RelayCommand<object>(ExecuteShowAssignView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);
            ShowUserInformationViewCommand = new RelayCommand<object>(ExecuteShowUserInformationViewCommand);
        }

        private void ExecuteShowUserInformationViewCommand(object obj)
        {
            currentChildView.Content = userInformationUC;
            StatusUserInformationView = true;
        }

        private void ExecuteShowSettingsView(object obj)
        {
            currentChildView.Content = settingsUC;
            StatusSettingsView = true;
        }

        private void ExecuteShowNotifyView(object obj)
        {
            currentChildView.Content = notifyUC;
            StatusNotifyView = true;    
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            currentChildView.Content = workScheduleUC;
            StatusWorkScheduleView = true;  
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            currentChildView.Content = employeesUC;
            StatusEmployeesView = true; 
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = assignUC;
            StatusAssignView = true;
        }
    }
}
