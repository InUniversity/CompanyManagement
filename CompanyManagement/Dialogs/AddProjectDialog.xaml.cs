using System.Windows;
using CompanyManagement.ViewModels;

namespace CompanyManagement.Dialogs;

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