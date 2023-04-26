using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public class LeaveListForDepartmentHead : ILeaveListStrategy
    {
        public void SetVisible(LeaveListViewModel viewModel)
        {
            viewModel.VisibleLeaveRequestListExpander = Visibility.Visible;
            viewModel.VisibleUnapprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleApprovedLeaveListExpander = Visibility.Visible;
            viewModel.VisibleDeniedLeaveListExpander = Visibility.Visible;
        }
    }
}