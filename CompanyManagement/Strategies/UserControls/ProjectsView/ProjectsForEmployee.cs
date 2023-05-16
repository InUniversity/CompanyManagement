using System.Collections.Generic;
using System.Windows;
using CompanyManagement.Database;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.ProjectsView
{
    public class ProjectsForEmployee : IProjectsStrategy
    {
        private ProjectAssignmentsDao assignmentsDao = new ProjectAssignmentsDao();
        
        public void SetVisible(ProjectsViewModel viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Collapsed;
            viewModel.VisibleDeleteButton = Visibility.Collapsed;
            viewModel.VisibleUpdateButton = Visibility.Collapsed; 
        }

        public List<Project> GetProjects(string employeeID)
        {
            return assignmentsDao.SearchProjectByEmployeeID(employeeID);
        }
    }
}