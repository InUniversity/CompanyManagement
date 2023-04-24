using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TimeTrackingUC.xaml
    /// </summary>
    public partial class TimeTrackingUC : UserControl
    {
        public TimeTrackingUC()
        {
            InitializeComponent();
            DataContext = new TimeTrackingViewModel();
        }
    }
}
