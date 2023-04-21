using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsForEmployee : IProjectsStrategy
    {
        private ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();
        
        public void SetVisible(IProjects viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Collapsed;
            viewModel.VisibleDeleteButton = Visibility.Collapsed;
            viewModel.VisibleUpdateButton = Visibility.Collapsed; 
        }

        public List<Project> GetProjects(string employeeID)
        {
            return projectAssignmentDao.SearchProjectByEmployeeID(employeeID);
        }
    }
}