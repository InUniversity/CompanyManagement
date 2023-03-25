using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels;

namespace CompanyManagement.UserControls;

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