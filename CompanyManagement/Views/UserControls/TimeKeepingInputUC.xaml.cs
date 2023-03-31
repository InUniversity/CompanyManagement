using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    /// Interaction logic for TimeKeepingInputUC.xaml
    /// </summary>
    public partial class TimeKeepingInputUC : UserControl
    {
        public TimeKeepingInputUC()
        {

            InitializeComponent();
            DataContext = new TimeKeepingInputViewModel();
        }
    }
}
