using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsForDepartmentHead : IProjectsStrategy
    {
        private ProjectAssignmentDao projectAssignmentDao = new ProjectAssignmentDao();
        
        public void SetVisible(IProjects viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Collapsed;
            viewModel.VisibleDeleteButton = Visibility.Collapsed;
            viewModel.VisibleUpdateButton = Visibility.Visible;
        }

        public List<Project> GetProjects(string employeeID)
        {
            return projectAssignmentDao.SearchProjectByEmployeeID(employeeID);
        }
    }
}
