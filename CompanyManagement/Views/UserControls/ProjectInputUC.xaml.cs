using System.Windows.Controls;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for ProjectInputUC.xaml
/// </summary>
public partial class ProjectInputUC : UserControl
{
    public ProjectInputUC()
    {
        InitializeComponent();
        DataContext = new ProjectInputViewModel();

    }
}