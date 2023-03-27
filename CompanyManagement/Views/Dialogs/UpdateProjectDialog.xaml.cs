using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for UpdateProjectDialog.xaml
/// </summary>
public partial class UpdateProjectDialog : Window
{
    public UpdateProjectDialog()
    {
        InitializeComponent();
        IProjectInput projectInput = new ProjectInputViewModel(new ProjectAssignmentDao());
        DataContext = new UpdateProjectViewModel(projectInput);
    }
}