using CompanyManagement.ViewModels.Windows;
using System.Windows;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainForHR : IMainStrategy
    {
        public void SetVisible(MainViewModel viewModel)
        {
            viewModel.VisibilityAssignment = Visibility.Collapsed;
            viewModel.VisibilityOrganization = Visibility.Visible;
            viewModel.VisibilityApproveLeaves = Visibility.Visible;
            viewModel.VisibilitySalaryRecords = Visibility.Visible;
        }
    }
}
