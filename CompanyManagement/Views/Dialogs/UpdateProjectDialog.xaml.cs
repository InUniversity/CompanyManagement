using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for UpdateProjectDialog.xaml
/// </summary>
public partial class UpdateProjectDialog : Window
{
    public UpdateProjectDialog()
    {
        InitializeComponent();
        DataContext = new UpdateProjectViewModel();
    }
}