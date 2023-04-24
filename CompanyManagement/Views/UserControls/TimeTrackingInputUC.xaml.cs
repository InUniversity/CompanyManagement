using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TimeTrackingInputUC.xaml
    /// </summary>
    public partial class TimeTrackingInputUC : UserControl
    {
        public TimeTrackingInputUC()
        {

            InitializeComponent();
            DataContext = new TimeTrackingInputViewModel();
        }
    }
}
