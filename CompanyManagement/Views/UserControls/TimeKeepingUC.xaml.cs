using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TimeKeepingUC.xaml
    /// </summary>
    public partial class TimeKeepingUC : UserControl
    {
        public TimeKeepingUC()
        {
            InitializeComponent();
            DataContext = new TimeKeepingViewModel(new TimeKeepingDao());
        }
    }
}
