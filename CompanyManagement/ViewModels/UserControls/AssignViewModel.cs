using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IAsignments
    {
        void ShowTasksView();
    }

    public class AssignViewModel : BaseViewModel , IAsignments
    {

        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); } }

        private Visibility visibilityMenu = Visibility.Hidden;
        public Visibility VisibilityMenu { get => visibilityMenu; set { visibilityMenu = value; OnPropertyChanged(); } }

        private TasksInProjectUC taskView = new TasksInProjectUC();

        private TimeKeepingUC timeKeepingView = new TimeKeepingUC();
        public ICommand ShowProjectsView { get; }

        public ICommand ShowTasksView { get; }

        public ICommand ShowTimeKeeping { get; }

        private bool statusTasksView = false;

        public bool StatusTasksView { get => statusTasksView; set { statusTasksView = value; OnPropertyChanged(); } }

        private bool statusTimeKeepingView = false;
        public bool StatusTimeKeepingView { get => statusTimeKeepingView; set { statusTimeKeepingView = value; OnPropertyChanged(); } }

        public AssignViewModel() 
        {
            showProjectView();          
            ShowProjectsView = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowTasksView = new RelayCommand<object>(ExecuteShowTasksView);
            ShowTimeKeeping = new RelayCommand<object>(ExecuteShowTimeKeepingView);
        }

        private void ExecuteShowTimeKeepingView(object obj)
        {
            CurrentChildView = timeKeepingView;
            StatusTimeKeepingView = true;
        }

        private void ExecuteShowProjectsView(object obj)
        {
            showProjectView();
        }
        private void ExecuteShowTasksView(object obj)
        {
            showTasksView();
        }

        void showProjectView()
        {
            CurrentChildView = new ProjectsUC();
            ((ProjectsViewModel)CurrentChildView.DataContext).parentDataContext = this;
            VisibilityMenu = Visibility.Hidden;
        }

        void showTasksView()
        {     
            CurrentChildView= taskView;
            VisibilityMenu = Visibility.Visible;
            StatusTasksView = true;
        }

        void IAsignments.ShowTasksView()
        {
            taskView.DataContext = ((ProjectsViewModel)CurrentChildView.DataContext).TasksDataContext;
            showTasksView();
        }
    }
}
