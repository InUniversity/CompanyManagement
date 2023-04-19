using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for LeaveInputUC.xaml
    /// </summary>
    public partial class LeaveInputUC : UserControl
    {
        public LeaveInputUC()
        {
            InitializeComponent();
            DataContext = new LeaveViewModel();
        }
    }
}
