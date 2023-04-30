using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ApproveLeaveRequestListUC.xaml
    /// </summary>
    public partial class ApproveLeaveRequestListUC : UserControl
    {
        public ApproveLeaveRequestListUC()
        {
            InitializeComponent();
            DataContext = new ApproveLeaveRequestListViewModel();
        }
    }
}
