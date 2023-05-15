using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for WidgetsUC.xaml
    /// </summary>
    public partial class WidgetsUC : UserControl
    {
        public WidgetsUC()
        {
            InitializeComponent();
            DataContext = new WidgetsViewModel();
        }

      
    }
}
