using CompanyManagement.ViewModels;
using System.Windows.Controls;

namespace CompanyManagement.UserControls
{
    /// <summary>
    /// Interaction logic for ProjectInputUC.xaml
    /// </summary>
    public partial class ProjectInputUC : UserControl
    {
        public ProjectInputUC()
        {
            InitializeComponent();
            DataContext = new ProjectInputViewModel();
        }
    }
}
