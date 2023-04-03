using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface IRetrieveProjectID
    {
        void RetrieveProjectID(string projectID);
    }
    
    public interface IProjectDetails
    {
        INavigateAssignmentView ParentDataContext { set; } 
    }
    
    public class ProjectDetailsViewModel : BaseViewModel, IProjectDetails, IRetrieveProjectID
    {
        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        public ICommand BackProjectsViewCommand { get; set; }
        public ICommand ShowTasksViewCommand { get; set; }
        public ICommand ShowTimeKeepingCommand { get; set; }

        private TasksInProjectUC taskView = new TasksInProjectUC();
        private TimeKeepingUC timeKeepingView = new TimeKeepingUC();

        private bool statusTasksView = false;
        public bool StatusTasksView { get => statusTasksView; set { statusTasksView = value; OnPropertyChanged(); } }

        private bool statusTimeKeepingView = false;
        public bool StatusTimeKeepingView { get => statusTimeKeepingView; set { statusTimeKeepingView = value; OnPropertyChanged(); } }

        public INavigateAssignmentView ParentDataContext { get; set; }

        private string projectID = "";
        
        public ProjectDetailsViewModel()
        {
            SetCommands();
        }

        private void SetCommands()
        {
            BackProjectsViewCommand = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowTasksViewCommand = new RelayCommand<object>(_ => ShowTasksView());
            ShowTimeKeepingCommand = new RelayCommand<object>(ExecuteShowTimeKeepingView);
        }

        private void ExecuteShowProjectsView(object obj)
        {
            ParentDataContext.MoveToProjectsView();
        }

        private void ShowTasksView()
        {
            CurrentChildView = taskView;
            StatusTasksView = true;
        }

        private void ExecuteShowTimeKeepingView(object obj)
        {
            CurrentChildView = timeKeepingView;
            StatusTimeKeepingView = true;
        }

        public void RetrieveProjectID(string projectID)
        {
            if (string.Equals(projectID, this.projectID))
                return;
            this.projectID = projectID;
            try
            {
                ((IRetrieveProjectID)taskView.DataContext).RetrieveProjectID(projectID);
                ((IRetrieveProjectID)timeKeepingView.DataContext).RetrieveProjectID(projectID);
            }
            catch 
            {
                Log.Instance.Error(nameof(ProjectDetailsViewModel), 
                    "The data context must have implement interface IRetrieveProjectID");
            }
            ShowTasksView();
        }
    }
}
