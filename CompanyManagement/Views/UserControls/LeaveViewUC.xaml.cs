using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveViewUC.xaml
    /// </summary>
    public partial class LeaveViewUC : UserControl
    {
        public LeaveViewUC()
        {
            InitializeComponent();
            DataContext = new LeaveInputViewModel();
        }
    }
}
