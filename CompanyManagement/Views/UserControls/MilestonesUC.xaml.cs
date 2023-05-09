using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MilestonesUC.xaml
    /// </summary>
    public partial class MilestonesUC : UserControl
    {
        public MilestonesUC()
        {
            InitializeComponent();
            DataContext = new MilestonesViewModel();
        }
    }
}
