using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Utilities;
using System.Windows;
using CompanyManagement.Models;

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
        public ICommand ShowMilestonesCommand { get; private set; }
        public ICommand ShowWidgetsViewCommand { get; private set; }

        private TasksInProjectUC tasksInProjectUC = new TasksInProjectUC();
        private ProjectBonusUC projectBonusUC = new ProjectBonusUC();
        private MilestonesUC milestonesUC = new MilestonesUC();
        private WidgetsUC widgetsUC = new WidgetsUC();

        private bool statusTasksView = false;
        public bool StatusTasksView { get => statusTasksView; set { statusTasksView = value; OnPropertyChanged(); } }

        private bool statusProjectBonusesView = false;
        public bool StatusProjectBonusesView { get => statusProjectBonusesView; set { statusProjectBonusesView = value; OnPropertyChanged(); } }
        
        private bool statusMilestonesView = false;
        public bool StatusMilestonesView { get => statusMilestonesView; set { statusMilestonesView = value; OnPropertyChanged(); } }
        
        private bool statusWidgetsView = false;
        public bool StatusWidgetsView { get => statusWidgetsView; set { statusWidgetsView = value; OnPropertyChanged(); } }

        private Visibility visibilityBonusProj;
        public Visibility VisibilityBonusProj { get => visibilityBonusProj; set { visibilityBonusProj = value; OnPropertyChanged(); } }

        public INavigateAssignmentView ParentDataContext { get; set; }

        public ProjectDetailsViewModel()
        {
            SetCommands();
            SetVisibilityViewBonusProj();
        }

        private void SetVisibilityViewBonusProj()
        {
            VisibilityBonusProj = CurrentUser.Ins.EmployeeIns.EmplRole.Perms == Enums.EPermission.Mgr ? Visibility.Visible : Visibility.Collapsed;
        }


        private void SetCommands()
        {
            BackProjectsViewCommand = new RelayCommand<object>(ExecuteShowProjectsView);
            ShowTasksViewCommand = new RelayCommand<object>(_ => ShowTasksView());
            ShowProjectBonusesCommand = new RelayCommand<object>(ExecuteShowProjectBonusesView);
            ShowMilestonesCommand = new RelayCommand<object>(ExecuteShowMilestonesView);
            ShowWidgetsViewCommand = new RelayCommand<object>(ExecuteShowWidgetsViewCommand);
        }

        private void ExecuteShowWidgetsViewCommand(object obj)
        {
            ((WidgetsViewModel)widgetsUC.DataContext).LoadLiveChartViews();
            CurrentChildView = widgetsUC;
            StatusWidgetsView = true;
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

        private void ExecuteShowProjectBonusesView(object obj)
        {
            CurrentChildView = projectBonusUC;
            StatusProjectBonusesView = true;
        }

        private void ExecuteShowMilestonesView(object obj)
        {
            CurrentChildView = milestonesUC;
            statusMilestonesView = true;
        }    

        public void ReceiveProjectID(string projectID)
        {
            ShowTasksView();
            try
            {
                ((IRetrieveProjectID)tasksInProjectUC.DataContext).ReceiveProjectID(projectID);
                ((IRetrieveProjectID)projectBonusUC.DataContext).ReceiveProjectID(projectID);
                ((IRetrieveProjectID)milestonesUC.DataContext).ReceiveProjectID(projectID);
                ((IRetrieveProjectID)widgetsUC.DataContext).ReceiveProjectID(projectID);
            }
            catch 
            {
                Log.Instance.Error(nameof(ProjectDetailsViewModel), 
                    "The data context must have implement interface IRetrieveProjectID");
            }
        }
    }
}
