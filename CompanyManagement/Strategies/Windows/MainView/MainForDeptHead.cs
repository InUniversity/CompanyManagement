using System.Windows;
using CompanyManagement.ViewModels.Windows;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainForDeptHead : IMainStrategy
    {
        public void SetVisible(MainViewModel viewModel)
        {
            viewModel.VisibilityAssignment = Visibility.Visible;
            viewModel.VisibilityOrganization = Visibility.Visible;
            viewModel.VisibilityApproveLeaves = Visibility.Visible;
            viewModel.VisibilitySalaryRecords = Visibility.Collapsed;
        }
    }
}
