using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    ///     Interaction logic for CheckInOutUC.xaml
    /// </summary>
    public partial class CheckInOutUC : UserControl
    {
        public CheckInOutViewModel ViewModel { get => (CheckInOutViewModel)DataContext; }

        public CheckInOutUC()
        {
            InitializeComponent();
            DataContext = new CheckInOutViewModel();
        }
    }
}
