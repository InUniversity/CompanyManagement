using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveUC.xaml
    /// </summary>
    public partial class LeaveListUC : UserControl
    {
        public LeaveListUC()
        {
            InitializeComponent();
            DataContext = new LeaveListViewModel();
        }
    }
}
