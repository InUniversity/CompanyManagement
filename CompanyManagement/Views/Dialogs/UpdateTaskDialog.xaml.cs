using System.Windows;
using CompanyManagement.Database.Implementations;
using CompanyManagement.ViewModels.Dialogs;
using CompanyManagement.ViewModels.UserControls;

namespace CompanyManagement.Views.Dialogs;

/// <summary>
///     Interaction logic for UpdateTaskInProject.xaml
/// </summary>
public partial class UpdateTaskDialog : Window
{
    public UpdateTaskDialog()
    {
        InitializeComponent();
        DataContext = new UpdateTaskViewModel();
    }
}