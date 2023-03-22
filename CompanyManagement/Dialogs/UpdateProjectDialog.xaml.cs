using System.Windows;
using CompanyManagement.ViewModels;

namespace CompanyManagement.Dialogs;

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