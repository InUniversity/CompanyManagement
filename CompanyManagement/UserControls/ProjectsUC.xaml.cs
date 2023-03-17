using CompanyManagement.ViewModels;
using System.Windows.Controls;

namespace CompanyManagement.UserControls;

/// <summary>
///     Interaction logic for _ProjectUC.xaml
/// </summary>
public partial class ProjectsUC : UserControl
{
    public ProjectsUC()
    {
        InitializeComponent();
        ((ProjectsViewModel)DataContext).TasksDataContext = new TasksInProjectViewModel();
    }
}