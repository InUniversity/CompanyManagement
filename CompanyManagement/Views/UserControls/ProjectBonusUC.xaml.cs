using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectSalaryBonusUC.xaml
    /// </summary>
    public partial class ProjectBonusUC : UserControl
    {
        public ProjectBonusUC()
        {
            InitializeComponent();
            DataContext = new ProjectBonusViewModel();
        }
    }
}
