using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IRetrieveProjectID
    {
        void ReceiveProjectID(string projectID);
    }
    
    public interface IProjectDetails
    {
        INavigateAssignmentView ParentDataContext { set; } 
    }
    
    public class ProjectDetailsViewModel : BaseViewModel, IProjectDetails, IRetrieveProjectID
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public ICommand BackProjectsViewCommand { get; private set; }
        public ICommand ShowTasksViewCommand { get; private set; }
        public ICommand ShowTimeKeepingCommand { get; private set; }
        public ICommand ShowProjectBonusesCommand { get; private set; }

        private TasksInProjectUC tasksInProjectUC = new TasksInProjectUC();
        private TimeTrackingUC timeTrackingUC = new TimeTrackingUC();
        private ProjectBonusUC projectBonusUC = new ProjectBonusUC();

        private bool statusTasksView = false;
        public bool StatusTasksView { get => statusTasksView; set { statusTasksView = value; OnPropertyChanged(); } }

        private bool statusTimeKeepingView = false;
        public bool StatusTimeKeepingView { get => statusTimeKeepingView; set { statusTimeKeepingView = value; OnPropertyChanged(); } }

        private bool statusProjectBonusesView = false;
        public bool StatusProjectBonusesView { get => statusProjectBonusesView; set { statusProjectBonusesView = value; OnPropertyChanged(); } }

        public INavigateAssignmentView ParentDataContext { get; set; }

        public ProjectDetailsViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            BackProjectsViewCommand = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowTasksViewCommand = new RelayCommand<object>(_ => ShowTasksView());
            ShowTimeKeepingCommand = new RelayCommand<object>(ExecuteShowTimeKeepingView);
            ShowProjectBonusesCommand = new RelayCommand<object>(ExecuteShowProjectBonusesView);
        }

        private void ExecuteShowProjectsView(object obj)
        {
            ParentDataContext.MoveToProjectsView();
            Log.Instance.Information(nameof(ProjectsViewModel), "Back to project view");
        }

        private void ShowTasksView()
        {
            CurrentChildView = tasksInProjectUC;
            StatusTasksView = true;
        }

        private void ExecuteShowTimeKeepingView(object obj)
        {
            CurrentChildView = timeTrackingUC;
            StatusTimeKeepingView = true;
        }

        private void ExecuteShowProjectBonusesView(object obj)
        {
            CurrentChildView = projectBonusUC;
            StatusProjectBonusesView = true;
        }

        public void ReceiveProjectID(string projectID)
        {
            ShowTasksView();
            try
            {
                ((IRetrieveProjectID)tasksInProjectUC.DataContext).ReceiveProjectID(projectID);
                ((IRetrieveProjectID)timeTrackingUC.DataContext).ReceiveProjectID(projectID);
                ((IRetrieveProjectID)projectBonusUC.DataContext).ReceiveProjectID(projectID);
            }
            catch 
            {
                Log.Instance.Error(nameof(ProjectDetailsViewModel), 
                    "The data context must have implement interface IRetrieveProjectID");
            }
        }
    }
}
