using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsForManager : IProjectsStrategy
    {
        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();

        public void SetVisible(ProjectsViewModel viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Visible;
            viewModel.VisibleDeleteButton = Visibility.Visible;
            viewModel.VisibleUpdateButton = Visibility.Visible; 
        }

        public List<Project> GetProjects(string employeeID)
        {
            return assignmentsDao.SearchProjectByCreatorID(employeeID);
        }
    }
}
