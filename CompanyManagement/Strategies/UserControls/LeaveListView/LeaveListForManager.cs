using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForManager : ILeaveListStrategy
    {
        public void SetVisible(LeaveRequestsViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestsExpander = Visibility.Collapsed;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleApprovedLeaveRequesrsExpander = Visibility.Visible;
            viewModel.VisibleDeniedLeaveRequestsExpander = Visibility.Visible;
        }
    }
}