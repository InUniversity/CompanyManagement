using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for AssignmentUC.xaml
    /// </summary>
    public partial class AssignmentUC : UserControl
    {
        public AssignmentUC()
        {
            InitializeComponent();
            DataContext = new AssignmentViewModel();
        }
    }
}
