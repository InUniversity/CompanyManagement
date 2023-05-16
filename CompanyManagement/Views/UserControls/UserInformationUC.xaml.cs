using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for UserInformationUC.xaml
    /// </summary>
    public partial class UserInformationUC : UserControl
    {
        public UserInformationUC()
        {
            InitializeComponent();
            DataContext = new UserInformationViewModel();
        }
    }
}
