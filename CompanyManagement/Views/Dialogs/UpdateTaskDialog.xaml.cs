using System.Windows;
using CompanyManagement.ViewModels.Dialogs;

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