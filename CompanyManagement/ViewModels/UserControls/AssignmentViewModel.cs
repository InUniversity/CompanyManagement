using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Input;
using CompanyManagement.Utilities;

namespace CompanyManagement.ViewModels.UserControls
{
    public interface INavigateAssignmentView
    {
        void MoveToProjectsView();
        void MoveToProjectDetailsView();
    }

    public class AssignmentViewModel : BaseViewModel, INavigateAssignmentView
    {

        private ContentControl currentChildView;
        public ContentControl CurrentChildView { get => currentChildView; set { currentChildView = value; OnPropertyChanged(); } }

        private ProjectsUC projectsUC = new ProjectsUC();
        private ProjectDetailsUC projectDetailsUC = new ProjectDetailsUC();

        private static int createdTimes = 0;

        public AssignmentViewModel()
        {
            Log.Instance.Information(nameof(AssignmentViewModel), createdTimes.ToString());
            createdTimes++;
            CurrentChildView = projectsUC;
            ((IProjects)projectsUC.DataContext).ParentDataContext = this;
            ((IProjects)projectsUC.DataContext).ProjectDetailsDataContext = (IRetrieveProjectID)projectDetailsUC.DataContext;
            ((IProjectDetails)projectDetailsUC.DataContext).ParentDataContext = this;
        }

        public void MoveToProjectsView()
        {
            CurrentChildView = projectsUC;
        }

        public void MoveToProjectDetailsView()
        {
            CurrentChildView = projectDetailsUC;
        }
    }
}
