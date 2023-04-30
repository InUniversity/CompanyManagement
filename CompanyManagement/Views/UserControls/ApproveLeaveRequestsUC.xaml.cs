using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ApproveLeaveRequestListUC.xaml
    /// </summary>
    public partial class ApproveLeaveRequestsUC : UserControl
    {
        public ApproveLeaveRequestsUC()
        {
            InitializeComponent();
            DataContext = new ApproveLeaveRequestsViewModel();
        }
    }
}
