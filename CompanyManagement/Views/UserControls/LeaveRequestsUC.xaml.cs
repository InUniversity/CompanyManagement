using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveUC.xaml
    /// </summary>
    public partial class LeaveRequestsUC : UserControl
    {
        public LeaveRequestsUC()
        {
            InitializeComponent();
            DataContext = new LeaveRequestsViewModel();
        }
    }
}
