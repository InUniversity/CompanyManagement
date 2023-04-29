using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForEmployee : ILeaveListStrategy
    {
        public void SetVisible(LeaveRequestsViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestsExpander = Visibility.Visible;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Collapsed;
            viewModel.VisibleApprovedLeaveRequesrsExpander = Visibility.Collapsed;
            viewModel.VisibleDeniedLeaveRequestsExpander = Visibility.Visible;
        }
    }
}
