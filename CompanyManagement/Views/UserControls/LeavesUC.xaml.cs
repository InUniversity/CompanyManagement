using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

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
