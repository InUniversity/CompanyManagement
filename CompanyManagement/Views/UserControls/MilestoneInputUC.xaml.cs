using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for MilestoneInputUC.xaml
    /// </summary>
    public partial class MilestoneInputUC : UserControl
    {
        public MilestoneInputUC()
        {
            InitializeComponent();
            DataContext = new MilestoneInputViewModel();
        }
    }
}
