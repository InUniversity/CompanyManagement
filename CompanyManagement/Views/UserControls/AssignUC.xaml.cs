using CompanyManagement.ViewModels.UserControls;
using System.Windows.Controls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for AssignUC.xaml
/// </summary>
public partial class AssignUC : UserControl
{
    public AssignUC()
    {
        InitializeComponent();
        DataContext = new AssignViewModel();
    }
}