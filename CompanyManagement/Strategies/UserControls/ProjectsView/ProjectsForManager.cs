using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsForManager : IProjectsStrategy
    {
        private ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();

        public void SetVisible(IProjects viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Visible;
            viewModel.VisibleDeleteButton = Visibility.Visible;
            viewModel.VisibleUpdateButton = Visibility.Visible; 
        }

        public List<Project> GetProjects(string employeeID)
        {
            return projectAssignmentDao.SearchProjectByCreatorID(employeeID);
        }
    }
}
