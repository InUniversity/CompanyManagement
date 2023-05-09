using System.Windows;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Strategies.UserControls.DeptsView
{
    public class DeptForHR : IDeptStrategy
    {
        public void SetVisible(DepartmentsViewModel viewModel)
        {
            viewModel.VisibleAddButton = Visibility.Visible;
            viewModel.VisibleDeleteButton = Visibility.Visible;        
        }
    }
}
