using System.Collections.Generic;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    public class ManagerViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new ProjectsUC();
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public ICommand ShowProjectsViewCommand { get; }
        public ICommand ShowEmployeesViewCommand { get; }
        public ICommand ShowWorkScheduleViewCommand { get; }
        public ICommand ShowNotifyViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public ICommand ShowTasksViewCommand { get; }
        public ICommand ShowTimeKeepingViewCommand { get; }

        public ManagerViewModel()
        {
            ShowProjectsViewCommand = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowEmployeesViewCommand = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkScheduleViewCommand = new RelayCommand<object>(ExecuteShowWorkScheduleView);
            ShowNotifyViewCommand = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsViewCommand = new RelayCommand<object>(ExecuteShowSettingsView);
            ShowTasksViewCommand = new RelayCommand<object>(ExecuteShowTasksView);
            ShowTimeKeepingViewCommand = new RelayCommand<object>(ExecuteShowTimeKeepingView);
        }

        private void ExecuteShowTimeKeepingView(object obj)
        {
            currentChildView.Content = new TimeKeepingUC();
        }

        private void ExecuteShowTasksView(object obj)
        {
            currentChildView.Content = new TasksInProjectUC();
        }

        private void ExecuteShowSettingsView(object obj)
        {
            currentChildView.Content = new SettingsUC();
        }

        private void ExecuteShowNotifyView(object obj)
        {
            currentChildView.Content = new NotifyUC();
        }

        private void ExecuteShowWorkScheduleView(object obj)
        {
            currentChildView.Content = new WorkScheduleUC();
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            currentChildView.Content = new EmployeesUC();
        }

        private void ExecuteShowProjectsView(object obj)
        {
            currentChildView.Content = new ProjectsUC();
        }
    }
}
