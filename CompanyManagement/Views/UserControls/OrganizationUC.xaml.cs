using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for OrganizationUC.xaml
    /// </summary>
    public partial class OrganizationUC : UserControl
    {
        public OrganizationUC()
        {
            InitializeComponent();
            DataContext = new OrganizationViewModel();
        }
    }
}
