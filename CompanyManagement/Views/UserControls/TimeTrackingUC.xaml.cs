using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for TimeTrackingUC.xaml
/// </summary>
public partial class TimeTrackingUC : UserControl
{
    public TimeTrackingUC()
    {
        InitializeComponent();
        DataContext = new TimeTrackingViewModel();
    }
}