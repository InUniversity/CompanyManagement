using CompanyManagement.Utilities;
using CompanyManagement.ViewModels.Base;
using CompanyManagement.Views.UserControls;
using System;
using System.Windows.Controls;

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

        private ProjectsUC projectsUC;
        private ProjectDetailsUC projectDetailsUC;

        public AssignmentViewModel()
        {
            InitAllView();
            SetParents();
            MoveToProjectsView();
        } 

        private void InitAllView()
        {
            projectsUC = new ProjectsUC();
            projectDetailsUC = new ProjectDetailsUC();
        }

        private void SetParents()
        {
            try
            {
                ((ProjectsViewModel)projectsUC.DataContext).ParentDataContext = this;
                ((ProjectsViewModel)projectsUC.DataContext).ProjectDetailsDataContext = (IRetrieveProjectID)projectDetailsUC.DataContext;
                ((IProjectDetails)projectDetailsUC.DataContext).ParentDataContext = this;
            }
            catch (Exception ex)
            {
                Log.Ins.Error(nameof(AssignmentViewModel), ex.Message);
            }
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
