using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls
{
    /// <summary>
    ///     Interaction logic for CheckInOutUC.xaml
    /// </summary>
    public partial class TimeSheetUC : UserControl
    {
        public TimeSheetViewModel ViewModel { get => (TimeSheetViewModel)DataContext; }

        public TimeSheetUC()
        {
            InitializeComponent();
            DataContext = new TimeSheetViewModel();
        }
    }
}
