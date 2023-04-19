using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeavesUC.xaml
    /// </summary>
    public partial class LeavesUC : UserControl
    {
        public LeavesUC()
        {
            // InitializeComponent();
            DataContext = new LeaveViewModel();
        }
    }
}
