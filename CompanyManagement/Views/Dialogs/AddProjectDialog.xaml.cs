using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for AddProjectDialog.xaml
/// </summary>
public partial class AddProjectDialog : Window
{
    public AddProjectDialog()
    {
        InitializeComponent();
        IProjectInput projectInput = new ProjectInputViewModel(new ProjectAssignmentDao(), new ProjectStatusDao());
        DataContext = new AddProjectViewModel(projectInput);
    }
}