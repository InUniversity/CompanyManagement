using CompanyManagement.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyManagement.Strategies.Windows.MainView
{
    public class MainForManager : IMainStrategy
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
