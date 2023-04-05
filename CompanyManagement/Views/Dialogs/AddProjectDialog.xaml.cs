using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for AddProjectDialog.xaml
/// </summary>
public partial class AddProjectDialog : Window
{
    public AddProjectDialog()
    {
        InitializeComponent();
        DataContext = new AddProjectViewModel();
    }
}