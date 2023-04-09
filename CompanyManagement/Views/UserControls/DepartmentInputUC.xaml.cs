using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for DepartmentInputUC.xaml
    /// </summary>
    public partial class DepartmentInputUC : UserControl
    {
        public DepartmentInputUC()
        {
            InitializeComponent();
            DataContext = new DepartmentInputViewModel();
        }
    }
}
