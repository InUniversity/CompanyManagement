using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForDepartmentHead : ILeaveListStrategy
    {
        public void SetVisible(LeaveRequestsViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestsExpander = Visibility.Visible;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleApprovedLeaveRequesrsExpander = Visibility.Visible;
            viewModel.VisibleDeniedLeaveRequestsExpander = Visibility.Visible;
        }
    }
}