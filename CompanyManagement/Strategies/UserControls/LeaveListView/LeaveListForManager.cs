using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForManager : ILeaveListStrategy
    {
        public void SetVisible(LeaveListViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestListExpander = Visibility.Collapsed;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleApprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleDeniedLeaveListExpander = Visibility.Visible;
        }
    }
}