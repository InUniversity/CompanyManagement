using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.LeaveListView
{
    public interface ILeaveListStrategy
    {
        void SetVisible(LeaveListViewModel viewModel);
    }
} 