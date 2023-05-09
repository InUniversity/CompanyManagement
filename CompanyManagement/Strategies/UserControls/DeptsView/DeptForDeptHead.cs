using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.DeptsView
{
    public class DeptForDeptHead : IDeptStrategy
    {
        public void SetVisible(DepartmentsViewModel viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Collapsed;
            viewModel.VisibleDeleteButton = Visibility.Collapsed;
        }
    }
}
