using System.Windows.Controls;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.UserControls;

/// <summary>
///     Interaction logic for _ProjectUC.xaml
/// </summary>
public partial class ProjectsUC : UserControl
{
    public ProjectsUC()
    {
        InitializeComponent();
        DataContext = new ProjectsViewModel(new ProjectDao());
    }
}