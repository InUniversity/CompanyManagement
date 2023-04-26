using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForEmployee : ILeaveListStrategy
    {
        public void SetVisible(LeaveListViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestListExpander = Visibility.Visible;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Collapsed;
            viewModel.VisibleApprovedLeaveListExpander = Visibility.Collapsed;
            viewModel.VisibleDeniedLeaveListExpander = Visibility.Visible;
        }
    }
}
