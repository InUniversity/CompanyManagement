using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for LeaveInputUC.xaml
/// </summary>
public partial class LeaveInputUC : UserControl
{
    public LeaveInputUC()
    {
        InitializeComponent();
        DataContext = new LeaveInputViewModel();
    }
}