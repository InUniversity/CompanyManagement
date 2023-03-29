using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.Windows
{
    class ManagerViewModel : BaseViewModel
    {
        private ContentControl currentChildView = new AssignUC();

        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        public ICommand ShowAssignView { get; }
        public ICommand ShowEmployeesView { get; }
        public ICommand ShowWorkSheduleView { get; }
        public ICommand ShowNotifytView { get; }
        public ICommand ShowSettingsView { get; }
        public ICommand ShowTasksView { get; }
        public ICommand ShowTimeKeepingView { get; }

        public ManagerViewModel()
        {
            ShowAssignView = new RelayCommand<object>(ExecuteShowAssignView);
            ShowEmployeesView = new RelayCommand<object>(ExecuteShowEmployeesView);
            ShowWorkSheduleView = new RelayCommand<object>(ExecuteShowWorkSheduleView);
            ShowNotifytView = new RelayCommand<object>(ExecuteShowNotifyView);
            ShowSettingsView = new RelayCommand<object>(ExecuteShowSettingsView);
            ShowTasksView = new RelayCommand<object>(ExecuteShowTasksView);
            ShowTimeKeepingView = new RelayCommand<object>(ExecuteShowTimeKeepingView);
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

        private void ExecuteShowWorkSheduleView(object obj)
        {
            currentChildView.Content = new WorkSheduleUC();
        }

        private void ExecuteShowEmployeesView(object obj)
        {
            currentChildView.Content = new EmployeesUC();
        }

        private void ExecuteShowAssignView(object obj)
        {
            currentChildView.Content = new AssignUC();
        }
    }
}
