using System.Windows;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainForEmployee : IMainStrategy
    {
        public void SetVisible(MainViewModel viewModel)
        {
            viewModel.VisibilityAssignment = Visibility.Visible;
            viewModel.VisibilityOrganization = Visibility.Collapsed;
            viewModel.VisibilityApproveLeaves = Visibility.Collapsed;
            viewModel.VisibilitySalaryRecords = Visibility.Collapsed;        
        }
    }
}